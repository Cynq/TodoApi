using Todo.Dal.Interfaces;

namespace Todo.Dal.Repositories
{
    public class AccountRepository : BaseRepository, IAccountRepository
    {
        public AccountRepository(TodoContext context) : base(context)
        {
            
        }
    }
}
