using Todo.Common.Models;
using Todo.Dal.Interfaces;

namespace Todo.Dal.Repositories
{
    public class AccountRepository : BaseRepository<User>, IAccountRepository<User>
    {
        public AccountRepository(TodoContext context) : base(context)
        {
            
        }
    }
}