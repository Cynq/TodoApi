using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Todo.Bll.Interfaces.Facades
{
    public interface IAccountFacade : IBaseFacade
    {
        Task<IdentityResult> CreateUserAsync(IdentityUser newUser, string modelPassword);
        Task SendConfirmationEmailAsync(string email, string confirmationToken);
        Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser newUser);
        Task<IdentityUser> FindUserByIdAsync(string id);
        Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string token);
        Task<IdentityUser> FindUserByEmailAsync(string email);
        Task<SignInResult> PasswordSignInAsync(IdentityUser user, string password, bool rememberMe, bool b);
        Task<string> GeneratePasswordResetTokenAsync(IdentityUser user);
        Task<IdentityResult> ResetPasswordAsync(IdentityUser user, string token, string password);
        Task SignOutAsync();
    }
}
