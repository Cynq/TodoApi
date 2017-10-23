using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Todo.Common.Models;

namespace Todo.Dal
{
    public class DbInitializer
    {
        public static void Initialize(TodoContext context, UserManager<User> userManager, SignInManager<User> signInManager, ILogger logger)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            CreateAdministrator(userManager, logger);

            var card = new TodoCard
            {
                Name = "Testowa karta 1",
                Tasks = new[]
                {
                    new TodoItem
                    {
                        Name = "Testowe zadanie 1"
                    },
                    new TodoItem
                    {
                        Name = "Testowe zadanie 2"
                    }
                }
            };

            context.Cards.Add(card);
            context.SaveChanges();
        }

        private static void CreateAdministrator(UserManager<User> userManager, ILogger logger)
        {
            var newUser = new User
            {
                UserName = "Administrator",
                Email = "admin@todo.pl",
                EmailConfirmed = true
            };

            var userCreationResult = userManager.CreateAsync(newUser, "tajneHasło1!").Result;

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
