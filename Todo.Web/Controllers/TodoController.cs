using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console.Internal;

namespace Todo.Web.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}