using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Models;

namespace Todo.Dal.Interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();
        TodoContext Context { get; set; }
        ITodoRepository<TodoItem> TodoRepository { get; set; }
        IAccountRepository<User> AccountRepository { get; set; }
    }
}
