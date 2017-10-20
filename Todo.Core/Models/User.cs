using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Todo.Common.Models
{
    public class User : IdentityUser
    {
        public ICollection<UserTodoItem> UserTodoItems { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}