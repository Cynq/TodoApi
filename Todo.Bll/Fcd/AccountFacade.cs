using Todo.Common.Interfaces.Facades;
using Todo.Common.Interfaces.Repositories;

namespace Todo.Bll.Fcd
{
    public class AccountFacade : BaseFacade, IAccountFacade
    {
        public IAccountRepository AccountRepository { get; }

        public AccountFacade(IAccountRepository accountRepository) : base(accountRepository)
        {
            AccountRepository = accountRepository;
        }
    }
}
