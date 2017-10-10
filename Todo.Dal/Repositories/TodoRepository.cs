using System.Linq;
using Todo.Common.Interfaces.Repositories;
using Todo.Common.Models;

namespace Todo.Dal.Repositories
{
    public class TodoRepository : BaseRepository, ITodoRepository
    {
        public TodoRepository(TodoContext context) : base(context)
        {
        }

        public void Update(TodoItem todo)
        {
            Context.TodoItems.Update(todo);
            SaveChanges();
        }

        public void Add(TodoItem item)
        {
            Context.TodoItems.Add(item);
            SaveChanges();
        }

        public IQueryable<TodoItem> GetAll()
        {
            return Context.TodoItems.AsQueryable();
        }

        public void Remove(TodoItem todo)
        {
            Context.TodoItems.Remove(todo);
            SaveChanges();
        }

        public TodoItem GetById(long id)
        {
            return Context.TodoItems.Find(id);
        }
    }
}
