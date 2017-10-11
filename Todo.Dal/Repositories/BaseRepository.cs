using Todo.Dal.Interfaces;

namespace Todo.Dal.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        // TODO use DI in future
        public TodoContext Context { get; set; }

        public BaseRepository(TodoContext context)
        {
            Context = context;
        }

        protected void SaveChanges()
        {
            Context.SaveChanges();
        }
    }
}
