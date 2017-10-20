using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Common.Models;

namespace Todo.Bll.Interfaces.Facades
{
    public interface ITodoFacade : IBaseFacade
    {
        IEnumerable<TodoItem> GetAll();
        Task<TodoItem> GetByIdAsync(long id);
        void Add(TodoItem item);
        void Update(TodoItem todo);
        void Remove(TodoItem todo);
    }
}
