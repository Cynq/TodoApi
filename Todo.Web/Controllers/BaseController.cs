using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Todo.Web.Controllers
{
    public class BaseController : Controller
    {
        public string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}