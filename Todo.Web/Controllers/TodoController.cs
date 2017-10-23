using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Bll.Interfaces.Facades;

namespace Todo.Web.Controllers
{
    [Authorize]
    public class TodoController : BaseController
    {
        public new ITodoFacade Facade { get; }

        public TodoController(ITodoFacade facade) : base(facade)
        {
            Facade = facade;
        }

        public IActionResult Index()
        {
            var model = Facade.GetAll();
            return View(model);
        }
    }
}