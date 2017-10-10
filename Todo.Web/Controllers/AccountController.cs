using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Common.Interfaces.Identity;

namespace Todo.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMessageService messageService) : base(userManager, signInManager, messageService)
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}