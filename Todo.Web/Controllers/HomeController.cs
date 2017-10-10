using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Common.Interfaces.Identity;
using Todo.Web.Models;

namespace Todo.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMessageService messageService) : base(userManager, signInManager, messageService)
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Logins()
        {
            return View();
        }
    }
}
