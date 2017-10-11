using System.Collections.Generic;
using System.Linq;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.Models;
using Todo.Dal.Interfaces;

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
