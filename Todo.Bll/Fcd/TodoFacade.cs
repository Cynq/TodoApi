using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.Models;
using Todo.Dal.Interfaces;

namespace Todo.Bll.Fcd
{
    public class TodoFacade : BaseFacade, ITodoFacade
    {
        public TodoFacade(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public IEnumerable<TodoItem> GetAll()
        {
            return UnitOfWork.TodoRepository.Get().ToList();
        }

        public async Task<TodoItem> GetByIdAsync(long id)
        {
            return await UnitOfWork.TodoRepository.GetById(id);
        }

        public void Add(TodoItem item)
        {
            UnitOfWork.TodoRepository.Add(item);
            UnitOfWork.SaveChanges();
        }

        public void Update(TodoItem todo)
        {
            UnitOfWork.TodoRepository.Update(todo);
            UnitOfWork.SaveChanges();
        }

        public void Remove(TodoItem todo)
        {
            UnitOfWork.TodoRepository.Delete(todo);
            UnitOfWork.SaveChanges();
        }
    }
}
