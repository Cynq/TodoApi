using System.Collections.Generic;
using System.Linq;
using Todo.Common.Interfaces.Facades;
using Todo.Common.Interfaces.Repositories;
using Todo.Common.Models;

namespace Todo.Bll.Fcd
{
    public class TodoFacade : BaseFacade, ITodoFacade
    {
        public new ITodoRepository Repository { get; }

        public TodoFacade(ITodoRepository repository) : base(repository)
        {
            Repository = repository;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        public TodoItem GetById(long id)
        {
            return Repository.GetById(id);
        }

        public void Add(TodoItem item)
        {
            Repository.Add(item);
        }

        public void Update(TodoItem todo)
        {
            Repository.Update(todo);
        }

        public void Remove(TodoItem todo)
        {
            Repository.Remove(todo);
        }
    }
}
