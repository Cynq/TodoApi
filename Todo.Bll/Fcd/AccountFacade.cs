using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Todo.Bll.Interfaces.Facades;
using Todo.Bll.Interfaces.Identity;
using Todo.Common.Models;
using Todo.Common.ViewModels;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Fcd
{
    public class AccountFacade : BaseFacade, IAccountFacade
    {
        protected readonly IMessageService MessageService;
        protected readonly UserManager<User> UserManager;
        protected readonly SignInManager<User> SignInManager;

        public AccountFacade(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IMessageService messageService) : base(unitOfWork, mapper)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            MessageService = messageService;
        }

        public async Task<IdentityResult> CreateUserAsync(User newUser, string password)
        {
            return await UserManager.CreateAsync(newUser, password);
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationToken)
        {
            await MessageService.Send(email, "Verify your email", $"Click <a href=\"{confirmationToken}\">here</a> to verify your email");
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(User newUser)
        {
            return await UserManager.GenerateEmailConfirmationTokenAsync(newUser);
        }

        public async Task<User> FindUserByIdAsync(string id)
        {
            return await UserManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(User user, string token)
        {
            return await UserManager.ConfirmEmailAsync(user, token);
        }

        public async Task<User> FindUserByEmailAsync(string email)
        {
            return await UserManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> PasswordSignInAsync(User user, string password, bool rememberMe, bool b)
        {
            return await SignInManager.PasswordSignInAsync(user, password, rememberMe, false);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(User user)
        {
            return await UserManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(User user, string token, string password)
        {
            return await UserManager.ResetPasswordAsync(user, token, password);
        }

        public async Task SignOutAsync()
        {
            await SignInManager.SignOutAsync();
        }

        public async Task<UserViewModel> GetUserVm(string userId)
        {
            var user = await GetCurrentUser();
            return Mapper.Map<User, UserViewModel>(user);
        }
    }
}