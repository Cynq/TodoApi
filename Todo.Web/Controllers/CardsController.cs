using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Bll.Interfaces.Facades;

namespace Todo.Web.Controllers
{
    [Authorize]
    public class CardsController : BaseController
    {
        public new ICardFacade Facade { get; }

        public CardsController(ICardFacade facade) : base(facade)
        {
            Facade = facade;
        }

        public IActionResult Index()
        {
            var model = Facade.Get();
            return View(model);
        }
    }
}