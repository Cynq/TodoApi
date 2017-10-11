using Todo.Bll.Interfaces.Facades;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Fcd
{
    public class BaseFacade : IBaseFacade
    {
        public IBaseRepository Repository { get; }

        public BaseFacade(IBaseRepository repository)
        {
            Repository = repository;
        }
    }
}