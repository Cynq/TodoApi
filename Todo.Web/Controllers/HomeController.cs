using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Todo.Common.ViewModels;

namespace Todo.Web.Controllers
{
    public class HomeController : Controller
    {
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
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            };
            // Get the details of the exception that occurred
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                // Get which route the exception occurred at
                model.Path = exceptionFeature.Path;

                // Get the exception that occurred
                model.Exception = exceptionFeature.Error;
            }

            return View(model);
        }

        public IActionResult Logins()
        {
            return View();
        }

        public IActionResult Info()
        {
            var model = new RedirectInfoViewModel
            {
                StatusCode = HttpContext.Response.StatusCode
            };

            return View(model);
        }
    }
}
