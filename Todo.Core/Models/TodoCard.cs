using System;
using System.Collections.Generic;

namespace Todo.Common.Models
{
    public class TodoCard
    {
        public long TodoCardId { get; set; }

        public string Name { get; set; }
        public int OrderNumber { get; set; }

        public User CreateUser { get; set; }
        public string CreateUserId { get; set; }
        public DateTime CreateTime { get; set; }

        public User DeleteUser { get; set; }
        public string DeleteUserId { get; set; }
        public DateTime DeleteTime { get; set; }

        public ICollection<TodoItem> Tasks { get; set; }
    }
}
