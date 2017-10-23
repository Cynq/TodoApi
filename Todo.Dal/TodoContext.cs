using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Common.Models;

namespace Todo.Dal
{
    public class TodoContext : IdentityDbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public new DbSet<User> Users { get; set; }
        public DbSet<UserTodoItem> UserTodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("TodoItem");
            modelBuilder.Entity<UserTodoItem>().ToTable("UserTodoItem");

            modelBuilder.Entity<UserTodoItem>()
                .HasKey(x => new {x.TodoItemId, x.UserId});

            modelBuilder.Entity<UserTodoItem>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserTodoItems);

            modelBuilder.Entity<UserTodoItem>()
                .HasOne(x => x.TodoItem)
                .WithMany(x => x.TodoItemUsersAuthorized);


            base.OnModelCreating(modelBuilder);
        }
    }
}
