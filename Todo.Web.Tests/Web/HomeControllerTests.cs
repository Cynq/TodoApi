using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Common.ViewModels;
using Todo.Web.Controllers;
using Xunit;

namespace Todo.Web.Tests.Web
{
    public class HomeControllerTests
    {
        private readonly HomeController _controller = new HomeController();
        
        [Fact]
        public void Index_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = _controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void About_ReurnsViewResultWithViewDataMessage()
        {
            // Arrange

            // Act
            var result = _controller.About();

            // Assert
            var ressult = Assert.IsType<ViewResult>(result);
            Assert.Equal(ressult.ViewData["Message"], "Your application description page.");
        }

        [Fact]
        public void Contacts_ReurnsViewResultWithViewDataMessage()
        {
            // Arrange

            // Act
            var result = _controller.Contact();

            // Assert
            var ressult = Assert.IsType<ViewResult>(result);
            Assert.Equal(ressult.ViewData["Message"], "Your contact page.");
        }

        [Fact]
        public void Logins_ReturnsViewResult()
        {
            // Arrange

            // Act
            var result = _controller.Logins();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Errors_ReturnsViewResultWithErrorViewModel()
        {
            // Arrange
            _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };

            // Act
            var result = _controller.Error();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsType<ErrorViewModel>(viewResult.ViewData.Model);
        }
    }
}
