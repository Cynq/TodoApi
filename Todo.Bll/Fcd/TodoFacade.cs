using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.Models;
using Todo.Common.ViewModels;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Fcd
{
    public class TodoFacade : BaseFacade, ITodoFacade
    {
        public TodoFacade(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

        public IEnumerable<TodoItemViewModel> GetAll()
        {
            return Mapper.Map<IEnumerable<TodoItem>, IEnumerable<TodoItemViewModel>>(UnitOfWork.TodoRepository.Get().ToList());
        }

        public async Task<TodoItemViewModel> GetByIdAsync(long id)
        {
            return Mapper.Map<TodoItem, TodoItemViewModel>(await UnitOfWork.TodoRepository.GetById(id));
        }

        public void Add(TodoItemViewModel item)
        {
            UnitOfWork.TodoRepository.Add(Mapper.Map<TodoItemViewModel, TodoItem>(item));
            UnitOfWork.SaveChanges();
        }

        public async Task Update(TodoItemViewModel model)
        {
            var todo = await UnitOfWork.TodoRepository.GetById(model.Id);
            UnitOfWork.TodoRepository.Update(Mapper.Map(model, todo));
            UnitOfWork.SaveChanges();
        }

        public void Remove(TodoItemViewModel todo)
        {
            UnitOfWork.TodoRepository.Delete(Mapper.Map<TodoItemViewModel, TodoItem>(todo));          
            UnitOfWork.SaveChanges();
        }
    }
}
