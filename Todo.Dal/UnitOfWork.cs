using Todo.Common.Models;
using Todo.Dal.Interfaces;

namespace Todo.Dal
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(TodoContext context, ITodoRepository<TodoItem> todoRepository, IAccountRepository<User> accountRepository)
        {
            Context = context;
            TodoRepository = todoRepository;
            AccountRepository = accountRepository;
        }

        #region Repositories

        public TodoContext Context { get; set; }
        public ITodoRepository<TodoItem> TodoRepository { get; set; }
        public IAccountRepository<User> AccountRepository { get; set; }

        #endregion

        public void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}