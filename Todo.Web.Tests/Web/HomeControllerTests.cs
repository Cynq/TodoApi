using Microsoft.AspNetCore.Mvc;
using Todo.Web.Controllers;
using Xunit;

namespace Todo.Web.Tests.Web
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AboutReurnsAWiewResultWithViewDataMessage()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.About();

            // Assert
            var ressult = Assert.IsType<ViewResult>(result);
            Assert.Equal(ressult.ViewData["Message"], "Your application description page.");
        }
    }
}
