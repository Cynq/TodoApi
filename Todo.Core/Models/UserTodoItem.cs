using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Common.Models
{
    public class UserTodoItem
    {
        public string UserId { get; set; }
        public User User { get; set; }
        public long TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; }
    }
}
