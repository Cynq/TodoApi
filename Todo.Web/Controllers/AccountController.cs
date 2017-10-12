using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.Models;

namespace Todo.Web.Controllers
{
    public class AccountController : BaseController
    {
        public IAccountFacade Facade { get; }

        public AccountController(IAccountFacade facade)
        {
            Facade = facade;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationModel model)
        {
            if (model.Password != model.PasswordConfirmation)
            {
                ModelState.AddModelError("PasswordDontMatch", "Password don't match");
                return View();
            }

            var newUser = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            var userCreationResult = await Facade.CreateUserAsync(newUser, model.Password);
            if (!userCreationResult.Succeeded)
            {
                foreach (var error in userCreationResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View();
            }

            var emailConfirmationToken = await Facade.GenerateEmailConfirmationTokenAsync(newUser);
            var tokenVerificationUrl = Url.Action("VerifyEmail", "Account", new { id = newUser.Id, token = emailConfirmationToken }, Request.Scheme);

            await Facade.SendConfirmationEmailAsync(model.Email, tokenVerificationUrl);

            return Content("Check your email for a verification link");
        }

        public async Task<IActionResult> VerifyEmail(string id, string token)
        {
            var user = await Facade.FindUserByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException();

            var emailConfirmationResult = await Facade.ConfirmEmailAsync(user, token);
            if (!emailConfirmationResult.Succeeded)
                return Content(emailConfirmationResult.Errors.Select(error => error.Description).Aggregate((allErrors, error) => allErrors + ", " + error));

            return Content("Email confirmed, you can now log in");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
            var user = await Facade.FindUserByEmailAsync(email);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return View();
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError(string.Empty, "Confirm your email first");
                return View();
            }

            var passwordSignInResult = await Facade.PasswordSignInAsync(user, password, rememberMe, false);
            if (!passwordSignInResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Invalid login");
                return View();
            }

            return Redirect("~/");
        }

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await Facade.FindUserByEmailAsync(email);
            if (user == null)
                return Content("Check your email for a password reset link");

            var passwordResetToken = await Facade.GeneratePasswordResetTokenAsync(user);
            var passwordResetUrl = Url.Action("ResetPassword", "Account", new { id = user.Id, token = passwordResetToken }, Request.Scheme);

            await Facade.SendConfirmationEmailAsync(email, passwordResetUrl);

            return Content("Check your email for a password reset link");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(string id, string token, string password, string repassword)
        {
            var user = await Facade.FindUserByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException();

            if (password != repassword)
            {
                ModelState.AddModelError(string.Empty, "Passwords do not match");
                return View();
            }

            var resetPasswordResult = await Facade.ResetPasswordAsync(user, token, password);
            if (!resetPasswordResult.Succeeded)
            {
                foreach (var error in resetPasswordResult.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View();
            }

            return Content("Password updated");
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await Facade.SignOutAsync();
            return Redirect("~/");
        }
    }
}