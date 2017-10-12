using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.Models;
using Todo.Web.Controllers;
using Xunit;

namespace Todo.Web.Tests.Web
{
    public class AccountControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // Arrange
            var mockFacade = new Mock<IAccountFacade>();
            var controller = new AccountController(mockFacade.Object);
            
            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Register_ReturnsAViewResult()
        {
            // Arrange
            var mockRepo = new Mock<IAccountFacade>();
            var controller = new AccountController(mockRepo.Object);

            // Act
            var result = controller.Register();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task RegisterPost_ReturnsViewWithInvalidStateModel_WhenPasswordNotMatch()
        {
            // Arrange
            var mockFacade = new Mock<IAccountFacade>();
            var controller = new AccountController(mockFacade.Object);
            var model = new RegistrationModel
            {
                Password = "1",
                PasswordConfirmation = "2"
            };

            // Act
            var result = await controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var errorMessage = viewResult.ViewData.ModelState.Single(x => x.Key == "PasswordDontMatch").Value.Errors.Single().ErrorMessage;
            Assert.Equal(null, viewResult.ViewName);
            Assert.Equal(1, viewResult.ViewData.ModelState.Count);
            Assert.Equal("Password don't match", errorMessage);
        }

        [Fact]
        public async Task RegisterPost_ReturnsViewWitValidModelState_WhenModelIsValid()
        {
            // Arrange
            var mockFacade = new Mock<IAccountFacade>();
            var model = new RegistrationModel
            {
                Email = string.Empty,
                PasswordConfirmation = string.Empty,
                Password= string.Empty,
            };
            mockFacade.Setup(facade => facade.CreateUserAsync(It.IsNotNull<IdentityUser>(), It.IsNotNull<string>())).Returns(Task.FromResult(IdentityResult.Failed()));
            var controller = new AccountController(mockFacade.Object);

            // Act
            var result = await controller.Register(model);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(null, viewResult.ViewName);
        }

        [Fact]
        public async Task RegisterPost_ReturnsContentWithSuccessMsg_WhenPassMatchAndCreateUsrSuccess()
        {
            // Arrange
            var mockFacade = new Mock<IAccountFacade>();
            var model = new RegistrationModel
            {
                Email = string.Empty,
                PasswordConfirmation = string.Empty,
                Password = string.Empty,
            };
            mockFacade.Setup(facade => facade.CreateUserAsync(It.IsNotNull<IdentityUser>(), It.IsNotNull<string>())).Returns(Task.FromResult(IdentityResult.Success));
            mockFacade.Setup(facade => facade.GenerateEmailConfirmationTokenAsync(It.IsAny<IdentityUser>())).Returns(Task.FromResult(string.Empty));
            mockFacade.Setup(facade => facade.SendConfirmationEmailAsync(It.IsNotNull<string>(), It.IsNotNull<string>())).Returns(Task.FromResult(string.Empty));
            var mockUrlHelper = new Mock<IUrlHelper>();
            var controller = new AccountController(mockFacade.Object)
            {
                Url = mockUrlHelper.Object,
                ControllerContext = new ControllerContext {HttpContext = new DefaultHttpContext()}
            };
            controller.ControllerContext.HttpContext.Request.Scheme = "";

            // Act
            var result = await controller.Register(model);

            // Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            mockFacade.Verify(m => m.CreateUserAsync(It.IsNotNull<IdentityUser>(), It.IsNotNull<string>()), Times.Once); // TODO: Zweryfikować czy to dobrze działa
            Assert.Equal("Check your email for a verification link", contentResult.Content);
        }
    }
}