using Todo.Bll.Interfaces.Facades;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Fcd
{
    public class BaseFacade : IBaseFacade
    {
        public BaseFacade(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public IUnitOfWork UnitOfWork { get; set; }
    }
}