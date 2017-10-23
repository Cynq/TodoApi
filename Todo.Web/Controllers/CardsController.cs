using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Bll.Interfaces.Facades;

namespace Todo.Web.Controllers
{
    [Authorize]
    public class CardsController : BaseController
    {
        public new ITodoFacade Facade { get; }

        public CardsController(ITodoFacade facade) : base(facade)
        {
            Facade = facade;
        }

        public IActionResult Index()
        {
            var model = Facade.UnitOfWork.Get();
            return View(model);
        }
    }
}