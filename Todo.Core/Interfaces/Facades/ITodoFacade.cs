using System.Collections.Generic;
using Todo.Common.Interfaces.Repositories;
using Todo.Common.Models;

namespace Todo.Common.Interfaces.Facades
{
    public interface ITodoFacade : IBaseFacade
    {
        IEnumerable<TodoItem> GetAll();
        TodoItem GetById(long id);
        void Add(TodoItem item);
        void Update(TodoItem todo);
        void Remove(TodoItem todo);
    }
}
