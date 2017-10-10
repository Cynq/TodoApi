using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Common.Interfaces.Repositories;

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
