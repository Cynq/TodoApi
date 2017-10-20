using Todo.Common.Models;
using Todo.Dal.Interfaces;

namespace Todo.Dal.Repositories
{
    public class TodoRepository : BaseRepository<TodoItem>, ITodoRepository<TodoItem>
    {
        public TodoRepository(TodoContext context) : base(context)
        {
        }
    }
}
