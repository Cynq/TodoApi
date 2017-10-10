using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Common.Interfaces.Identity;

namespace Todo.Web.Controllers
{
    public class BaseController : Controller
    {
        protected readonly UserManager<IdentityUser> UserManager;
        protected readonly SignInManager<IdentityUser> SignInManager;
        protected readonly IMessageService MessageService;

        public BaseController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMessageService messageService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            MessageService = messageService;
        }
    }
}