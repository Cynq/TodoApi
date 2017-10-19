using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Todo.Common.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string UserCreateId { get; set; }
        public User UserCreate { get; set; }
        public ICollection<UserTodoItem> TodoItemUsersAuthorized { get; set; }
    }
}
