using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Todo.Common.Models;

namespace Todo.Dal
{
    public class DbInitializer
    {
        public static async void Initialize(TodoContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger logger)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            await CreateAdministrator(userManager, logger);

            var todoItems = new[]
            {
                new TodoItem
                {
                    Name = "Testowe zadanie"
                }
            };

            context.TodoItems.AddRange(todoItems);
            context.SaveChanges();
        }

        private static async Task CreateAdministrator(UserManager<IdentityUser> userManager, ILogger logger)
        {
            var newUser = new IdentityUser
            {
                UserName = "Administrator",
                Email = "admin@todo.pl",
                EmailConfirmed = true
            };

            var userCreationResult = await userManager.CreateAsync(newUser, "tajneHasło1!");

            if (!userCreationResult.Succeeded)
            {
                foreach (var identityError in userCreationResult.Errors)
                {
                    logger.LogCritical($"Can't create admin account. Code number: {identityError.Code}, Message description: {identityError.Description}");
                }
            }
        }
    }
}
