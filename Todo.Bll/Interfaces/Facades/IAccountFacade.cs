using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Todo.Common.Models;
using Todo.Common.ViewModels;

namespace Todo.Bll.Interfaces.Facades
{
    public interface IAccountFacade : IBaseFacade
    {
        Task<IdentityResult> CreateUserAsync(User newUser, string modelPassword);
        Task SendConfirmationEmailAsync(string email, string confirmationToken);
        Task<string> GenerateEmailConfirmationTokenAsync(User newUser);
        Task<User> FindUserByIdAsync(string id);
        Task<IdentityResult> ConfirmEmailAsync(User user, string token);
        Task<User> FindUserByEmailAsync(string email);
        Task<SignInResult> PasswordSignInAsync(User user, string password, bool rememberMe, bool b);
        Task<string> GeneratePasswordResetTokenAsync(User user);
        Task<IdentityResult> ResetPasswordAsync(User user, string token, string password);
        Task SignOutAsync();
        Task <UserViewModel> GetUserVm(string userId);
    }
}
