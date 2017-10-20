using AutoMapper;
using Todo.Bll.Interfaces.Facades;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Fcd
{
    public class BaseFacade : IBaseFacade
    {
        public BaseFacade(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; }
    }
}