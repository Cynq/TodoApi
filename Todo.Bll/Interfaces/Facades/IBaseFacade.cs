using System.Threading.Tasks;
using AutoMapper;
using Todo.Bll.Fcd;
using Todo.Common.Models;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Interfaces.Facades
{
    public interface IBaseFacade
    {
        IUnitOfWork UnitOfWork { get; set; }
        IMapper Mapper { get; }
        BaseFacade.UserIdDelegate IdDelegate { get; set; }
        Task<User> GetCurrentUser();
    }
}
