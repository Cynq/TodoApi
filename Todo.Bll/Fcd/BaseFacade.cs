using AutoMapper;
using System.Threading.Tasks;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.Models;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Fcd
{
    public class BaseFacade : IBaseFacade
    {
        public IUnitOfWork UnitOfWork { get; set; }
        public IMapper Mapper { get; }
        public delegate string UserIdDelegate();
        public UserIdDelegate IdDelegate { get; set; }
        public string UserId
        {
            get
            {
                if (string.IsNullOrEmpty(_userId))
                    _userId = GetUserId();

                return _userId;
            }
        }
        private string _userId;

        protected BaseFacade(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<User> GetCurrentUser()
        {
            return await UnitOfWork.AccountRepository.GetById(UserId);
        }

        private string GetUserId()
        {
            return IdDelegate();
        }
    }
}