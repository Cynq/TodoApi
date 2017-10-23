using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Todo.Bll.Interfaces.Facades;

namespace Todo.Web.Controllers
{
    public class BaseController : Controller
    {
        public IBaseFacade Facade { get; set; }
        public string UserId => GetUserId();

        public BaseController(IBaseFacade facade)
        {
            Facade = facade;
            Facade.IdDelegate = GetUserId;
        }

        private string GetUserId()
        {
            return User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}