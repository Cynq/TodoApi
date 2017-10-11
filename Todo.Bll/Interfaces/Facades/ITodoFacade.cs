using System.Collections.Generic;
using Todo.Common.Models;

namespace Todo.Bll.Interfaces.Facades
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
