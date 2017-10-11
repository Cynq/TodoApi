using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Todo.Bll.Interfaces.Facades;
using Todo.Bll.Interfaces.Identity;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Fcd
{
    public class AccountFacade : BaseFacade, IAccountFacade
    {
        protected readonly IAccountRepository AccountRepository;
        protected readonly IMessageService MessageService;
        protected readonly UserManager<IdentityUser> UserManager;
        protected readonly SignInManager<IdentityUser> SignInManager;

        public AccountFacade(IAccountRepository accountRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMessageService messageService) : base(accountRepository)
        {
            AccountRepository = accountRepository;
            UserManager = userManager;
            SignInManager = signInManager;
            MessageService = messageService;
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser newUser, string password)
        {
            return await UserManager.CreateAsync(newUser, password);
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationToken)
        {
            await MessageService.Send(email, "Verify your email", $"Click <a href=\"{confirmationToken}\">here</a> to verify your email");
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser newUser)
        {
            return await UserManager.GenerateEmailConfirmationTokenAsync(newUser);
        }

        public async Task<IdentityUser> FindUserByIdAsync(string id)
        {
            return await UserManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string token)
        {
            return await UserManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityUser> FindUserByEmailAsync(string email)
        {
            return await UserManager.FindByEmailAsync(email);
        }

        public async Task<SignInResult> PasswordSignInAsync(IdentityUser user, string password, bool rememberMe, bool b)
        {
            return await SignInManager.PasswordSignInAsync(user, password, rememberMe, false);
        }

        public async Task<string> GeneratePasswordResetTokenAsync(IdentityUser user)
        {
            return await UserManager.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<IdentityResult> ResetPasswordAsync(IdentityUser user, string token, string password)
        {
            return await UserManager.ResetPasswordAsync(user, token, password);
        }

        public async Task SignOutAsync()
        {
            await SignInManager.SignOutAsync();
        }
    }
}