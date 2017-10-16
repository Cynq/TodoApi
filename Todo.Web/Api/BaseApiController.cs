using Microsoft.AspNetCore.Mvc;
using Todo.Bll.Interfaces.Facades;

namespace Todo.Web.Api
{
    public class BaseApiController : Controller
    {
        protected IBaseFacade Facade { get; }

        protected BaseApiController(IBaseFacade facade)
        {
            Facade = facade;
        }
    }
}
