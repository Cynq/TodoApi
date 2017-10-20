using AutoMapper;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Interfaces.Facades
{
    public interface IBaseFacade
    {
        IUnitOfWork UnitOfWork { get; set; }
        IMapper Mapper { get; }
    }
}
