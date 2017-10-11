using Todo.Common.Interfaces.Facades;
using Todo.Common.Interfaces.Repositories;

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