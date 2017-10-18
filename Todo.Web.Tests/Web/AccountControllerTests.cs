using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.Models;
using Todo.Web.Controllers;
using Xunit;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Todo.Web.Tests.Web
{
    public class AccountControllerTests
    {
        private string id = "";
        private string email = "";
        private string token = "";
        private string password = "";
        private string repassword = "";
        private readonly IdentityUser _usr = new IdentityUser();
        private readonly Mock<IUrlHelper> _mockUrlHelper = new Mock<IUrlHelper>();
        private readonly Mock<IAccountFacade> _mockFacade = new Mock<IAccountFacade>();
        private readonly AccountController _controller;

        public AccountControllerTests()
        {
            _controller = new AccountController(_mockFacade.Object);
        }

        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // Arrange

            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Register_ReturnsAViewResult()
        {
            // Arrange

            // Act
            var result = _controller.Register();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task RegisterPost_ReturnsViewWithInvalidStateModel_WhenPasswordNotMatch()
        {
            // Arrange
            var model = new RegistrationModel
            {
                Password = "1",
                PasswordConfirmation = "2"
            };

            // Act
            var result = await _controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var errorMessage = GetErrorMessage(viewResult, "PasswordDontMatch");
            Assert.Equal(null, viewResult.ViewName);
            Assert.Equal(1, viewResult.ViewData.ModelState.Count);
            Assert.Equal("Password don't match", errorMessage);
        }

        private static string GetErrorMessage(ViewResult viewResult, string errorName)
        {
            return viewResult.ViewData.ModelState.Single(x => x.Key == errorName).Value.Errors.Single().ErrorMessage;
        }

        [Fact]
        public async Task RegisterPost_ReturnsViewWithInvalidModelState_WhenModelIsInvalid()
        {
            // Arrange
            var model = new RegistrationModel
            {
                Email = string.Empty,
                PasswordConfirmation = string.Empty,
                Password = string.Empty,
            };
            var errors = new[]
            {
                new IdentityError
                {
                    Code = "errorCode",
                    Description = "errorDescription"
                }
            };
            var identityResult = IdentityResult.Failed(errors);
            _mockFacade.Setup(facade => facade.CreateUserAsync(It.IsNotNull<IdentityUser>(), It.IsNotNull<string>()))
                .Returns(Task.FromResult(identityResult));

            // Act
            var result = await _controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(errors.Count(), viewResult.ViewData.ModelState.Count);
            Assert.Equal(null, viewResult.ViewName);
        }

        [Fact]
        public async Task RegisterPost_ReturnsContentWithSuccessMsg_WhenPassMatchAndCreateUsrSuccess()
        {
            // Arrange
            var model = new RegistrationModel
            {
                Email = string.Empty,
                PasswordConfirmation = string.Empty,
                Password = string.Empty,
            };
            _mockFacade.Setup(facade => facade.CreateUserAsync(It.IsNotNull<IdentityUser>(), It.Is<string>(fn => fn.Equals(model.Password))))
                .Returns(Task.FromResult(IdentityResult.Success));
            _mockFacade.Setup(facade => facade.GenerateEmailConfirmationTokenAsync(It.IsAny<IdentityUser>()))
                .Returns(Task.FromResult(string.Empty));
            _mockFacade.Setup(facade => facade.SendConfirmationEmailAsync(It.Is<string>(fn => fn.Equals(model.Email)), It.IsAny<string>()))
                .Returns(Task.FromResult(string.Empty));

            var controller = new AccountController(_mockFacade.Object)
            {
                Url = _mockUrlHelper.Object,
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
            controller.ControllerContext.HttpContext.Request.Scheme = "";

            // Act
            var result = await controller.Register(model);

            // Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            _mockFacade.VerifyAll();
            Assert.Equal("Check your email for a verification link", contentResult.Content);
        }

        [Fact]
        public async Task VerifyEmail_ShouldThrowExceotion_WhenUserIsNull()
        {
            // Arrange
            _mockFacade.Setup(facade => facade.FindUserByIdAsync(id)).Returns(Task.FromResult((IdentityUser)null));

            // Act / Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.VerifyEmail(id, token));
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task VerifyEmail_ShouldReturnContentWithSuccessyfullMsg_WhenEmailConfirmed()
        {
            // Arrange
            _mockFacade.Setup(facade => facade.FindUserByIdAsync(id)).Returns(Task.FromResult(_usr));
            _mockFacade.Setup(facade => facade.ConfirmEmailAsync(_usr, token)).Returns(Task.FromResult(IdentityResult.Success));

            // Act
            var result = await _controller.VerifyEmail(id, token);

            // Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("Email confirmed, you can now log in", contentResult.Content);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task VerifyEmail_ReturnsContentWithErrorMessages_WhenErrorOccur()
        {
            // Arrange
            var errors = new[]
            {
                new IdentityError
                {
                    Code = "errorCode",
                    Description = "errorDescription"
                }
            };
            var identityResult = IdentityResult.Failed(errors);
            _mockFacade.Setup(facade => facade.FindUserByIdAsync(id)).Returns(Task.FromResult(_usr));
            _mockFacade.Setup(facade => facade.ConfirmEmailAsync(_usr, token)).Returns(Task.FromResult(identityResult));

            // Act
            var result = await _controller.VerifyEmail(id, token);

            // Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal(identityResult.Errors.Select(error => error.Description).Aggregate((allErrors, error) => allErrors + ", " + error), contentResult.Content);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task LoginPost_ShouldReturnInvalidLoginErrorInView_WhenNoUserFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.FindUserByEmailAsync(email)).Returns(Task.FromResult((IdentityUser)null));

            // Act
            var result = await _controller.Login(email, password, false);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var errorMessage = GetErrorMessage(viewResult, "InvalidLogin");
            Assert.Equal("Invalid login", errorMessage);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task LoginPost_ShouldReturnViewWithEmailNotConfirmedError_WhenEmailNotConfirmed()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.FindUserByEmailAsync(email)).Returns(Task.FromResult(_usr));

            // Act
            var result = await _controller.Login(email, password, false);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var errorMessage = GetErrorMessage(viewResult, "EmailNotConfirmed");
            Assert.Equal("Confirm your email first", errorMessage);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task LoginPost_ShouldReturnViewWithInvalidLogin_WhenLoginFailed()
        {
            // Arrange
            _usr.EmailConfirmed = true;
            var signinResult = SignInResult.Failed;
            _mockFacade.Setup(mock => mock.FindUserByEmailAsync(email)).Returns(Task.FromResult(_usr));
            _mockFacade.Setup(mock => mock.PasswordSignInAsync(_usr, password, false, false)).Returns(Task.FromResult(signinResult));

            // Act
            var result = await _controller.Login(email, password, false);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var errorMessage = GetErrorMessage(viewResult, "InvalidLogin");
            Assert.Equal("Invalid login", errorMessage);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task LoginPost_Redirect_AfterSuccessfullLogin()
        {
            // Arrange
            _usr.EmailConfirmed = true;
            var signinResult = SignInResult.Success;
            _mockFacade.Setup(mock => mock.FindUserByEmailAsync(email)).Returns(Task.FromResult(_usr));
            _mockFacade.Setup(mock => mock.PasswordSignInAsync(_usr, password, false, false)).Returns(Task.FromResult(signinResult));

            // Act
            var result = await _controller.Login(email, password, false);

            // Assert
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("~/", redirectResult.Url);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task ForgotPasswordPost_ReturnContentWithMessageWithoudSendingEmail_WhenNoUserFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.FindUserByEmailAsync(email)).Returns(Task.FromResult((IdentityUser)null));

            // Act
            var result = await _controller.ForgotPassword(email);

            // Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("Check your email for a password reset link", contentResult.Content);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task ForgotPasswordPost_ReturnContentWithMessageWithSendingEmail_WhenUserFound()
        {
            // Arrange
            _usr.Id = id;
            _mockUrlHelper.Setup(mock => mock.Action(It.IsAny<UrlActionContext>())).Returns("");
            _mockFacade.Setup(mock => mock.FindUserByEmailAsync(email)).Returns(Task.FromResult(_usr));
            _mockFacade.Setup(mock => mock.GeneratePasswordResetTokenAsync(_usr)).Returns(Task.FromResult(""));
            _mockFacade.Setup(mock => mock.SendConfirmationEmailAsync(email, It.IsAny<string>())).Returns(Task.FromResult(""));
            var controller = new AccountController(_mockFacade.Object)
            {
                Url = _mockUrlHelper.Object,
                ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() }
            };
            controller.ControllerContext.HttpContext.Request.Scheme = "";

            // Act
            var result = await controller.ForgotPassword(email);

            // Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("Check your email for a password reset link", contentResult.Content);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task ResetPassword_ThrowsInvalidOperationException_WhenNoUserFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.FindUserByIdAsync(id)).Returns(Task.FromResult((IdentityUser)null));

            // Act / Assert
            await Assert.ThrowsAsync<InvalidOperationException>(async () => await _controller.ResetPassword(id, token, password, repassword));
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task ResetPassword_ReturnsViewWithPasswordDontMatchError_WhenPassDontMatch()
        {
            // Arrange
            var newPassword = "2";
            var newRepassword = "1";
            _mockFacade.Setup(mock => mock.FindUserByIdAsync(id)).Returns(Task.FromResult(_usr));

            // Act
            var result = await _controller.ResetPassword(id, token, newPassword, newRepassword);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(null, viewResult.ViewName);
            var errorMessage = GetErrorMessage(viewResult, "PasswordDontMatch");
            Assert.Equal("Passwords do not match", errorMessage);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task ResetPassword_ReturnsContentWithSuccessFullMessage_WhenResetPassSuccess()
        {
            // Arrange
            var errors = new[]
            {
                new IdentityError
                {
                    Code = "errorCode",
                    Description = "errorDescription"
                }
            };
            var identityResult = IdentityResult.Failed(errors);
            _mockFacade.Setup(mock => mock.FindUserByIdAsync(id)).Returns(Task.FromResult(_usr));
            _mockFacade.Setup(mock => mock.ResetPasswordAsync(_usr, token, password)).Returns(Task.FromResult(identityResult));

            // Act
            var result = await _controller.ResetPassword(id, token, password, repassword);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var errorMessage = GetErrorMessage(viewResult, errors[0].Code);
            Assert.Equal(errors[0].Description, errorMessage);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task ResetPassword_ReturnsViewWithModelError_WhenResetPasswordFailed()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.FindUserByIdAsync(id)).Returns(Task.FromResult(_usr));
            _mockFacade.Setup(mock => mock.ResetPasswordAsync(_usr, token, password)).Returns(Task.FromResult(IdentityResult.Success));

            // Act
            var result = await _controller.ResetPassword(id, token, password, repassword);

            // Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("Password updated", contentResult.Content);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task LogOut_RunSignOutAsyncAndRedirectToRoot()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.SignOutAsync()).Returns(Task.FromResult(true));

            // Act
            var result = await _controller.Logout();

            // Assert
            var redirectResult = Assert.IsType<RedirectResult>(result);
            Assert.Equal("~/", redirectResult.Url);
            _mockFacade.Verify(mock => mock.SignOutAsync(), Times.Once);
        }

        [Fact]
        public async Task LoginGet_ReturnsView()
        {
            // Arrange

            // Act
            var result = _controller.Login();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(null, viewResult.ViewName);
        }

        [Fact]
        public async Task ForgotPasswordGet_ReturnsView()
        {
            // Arrange

            // Act
            var result = _controller.ForgotPassword();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(null, viewResult.ViewName);
        }
    }
}