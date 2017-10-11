using Microsoft.AspNetCore.Mvc;
using Todo.Bll.Interfaces.Facades;

namespace Todo.Web.Api
{
    public class BaseApiController : Controller
    {
        public IBaseFacade Facade { get; }

        public BaseApiController(IBaseFacade facade)
        {
            Facade = facade;
        }
    }
}
