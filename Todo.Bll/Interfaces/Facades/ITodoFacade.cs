using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Common.ViewModels;

namespace Todo.Bll.Interfaces.Facades
{
    public interface ITodoFacade : IBaseFacade
    {
        IEnumerable<TodoItemViewModel> GetAll();
        Task<TodoItemViewModel> GetByIdAsync(long id);
        void Add(TodoItemViewModel item);
        Task Update(TodoItemViewModel model);
        void Remove(TodoItemViewModel todo);
    }
}
