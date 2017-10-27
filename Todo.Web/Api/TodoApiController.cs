using Microsoft.AspNetCore.Mvc;
using Todo.Common.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.ViewModels;

namespace Todo.Web.Api
{
    [Route("api/[controller]")]
    public class TodoApiController : BaseApiController
    {
        private new ITodoFacade Facade { get; }

        public TodoApiController(ITodoFacade facade) : base(facade)
        {
            Facade = facade;
        }

        /// <summary>
        /// Returns all todoitem objects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<TodoItemViewModel> GetAll()
        {
            return Facade.GetAll();
        }

        /// <summary>
        /// Returns specific todoitem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<IActionResult> GetById(long id)
        {
            var item = await Facade.GetByIdAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        /// <summary>
        /// Creates posted todoitem
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create([FromBody] TodoItemViewModel item)
        {
            if (item == null)
            {
                return BadRequest();
            }

            Facade.Add(item);

            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        /// <summary>
        /// Updates todoitem data
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] TodoItemViewModel item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }

            var todo = await Facade.GetByIdAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            Facade.Update(item);
            return new OkResult();
        }

        /// <summary>
        /// Deletes an Todoitem object from db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var todo = await Facade.GetByIdAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            Facade.Remove(todo);
            return new OkResult();
        }
    }
}
