using System.Linq;
using Todo.Common.Models;

namespace Todo.Dal.Interfaces
{
    public interface ITodoRepository : IBaseRepository
    {
        void Update(TodoItem todo);
        void Add(TodoItem item);
        IQueryable<TodoItem> GetAll();
        void Remove(TodoItem todo);
        TodoItem GetById(long id);
    }
}
