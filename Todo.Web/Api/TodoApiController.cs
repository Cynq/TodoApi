using Microsoft.AspNetCore.Mvc;
using Todo.Common.Models;
using System.Collections.Generic;
using Todo.Bll.Interfaces.Facades;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Todo.Web.Api
{
    [Route("api/[controller]")]
    public class TodoApiController : BaseApiController
    {
        public new ITodoFacade Facade { get; set; }

        public TodoApiController(ITodoFacade facade) : base(facade)
        {
            Facade = facade;
        }

        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return Facade.GetAll();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public IActionResult GetById(long id)
        {
            var item = Facade.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TodoItem item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            Facade.Add(item);

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoItem item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = Facade.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            Facade.Update(todo);
            return new OkResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = Facade.GetById(id);
            if (todo == null)
            {
                return NotFound();
            }

            Facade.Remove(todo);
            return new OkResult();
        }
    }
}
