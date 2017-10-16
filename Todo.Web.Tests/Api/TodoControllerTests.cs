using Microsoft.AspNetCore.Mvc;
using Moq;
using Todo.Bll.Interfaces.Facades;
using Todo.Common.Models;
using Todo.Web.Api;
using Xunit;

namespace Todo.Web.Tests.Api
{
    public class TodoControllerTests
    {
        private readonly long _id = 1;
        private readonly Mock<ITodoFacade> _mockFacade = new Mock<ITodoFacade>();
        private readonly TodoController _controller;
        private readonly TodoItem _item = new TodoItem();

        public TodoControllerTests()
        {
            _controller = new TodoController(_mockFacade.Object);
        }

        [Fact]
        public void GetById_ReturnsNotForundResult_WhenUserNotFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.GetById(_id)).Returns((TodoItem)null);

            // Act
            var result = _controller.GetById(_id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void GetById_ReturnsObjectResultWithObject_WhenObjectFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.GetById(_id)).Returns(_item);

            // Act
            var result = _controller.GetById(_id);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(_item, objectResult.Value);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public void CreatePost_ReturnsBadRequest_WhenNoItemPosted()
        {
            // Arrange

            // Act
            var result =_controller.Create(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void CreatePost_ReturnsCreatedAtRouteResultWithItem_WhenItemPosted()
        {
            // Arrange
            _item.Id = _id;

            // Act
            var result = _controller.Create(_item);

            // Assert
            var createdAtRouteResult = Assert.IsType<CreatedAtRouteResult>(result);
            Assert.Equal("GetTodo", createdAtRouteResult.RouteName);
            Assert.Equal(_id, createdAtRouteResult.RouteValues["id"]);
            Assert.Equal(_item, createdAtRouteResult.Value);
        }

        [Fact]
        public void UpdatePut_ReturnsBadRequestResult_WhenPostedItemIsNull()
        {
            // Arrange
            
            // Act
            var result = _controller.Update(_id, null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void UpdatePut_ReturnsBadRequestResult_WhenIdsNotMatch()
        {
            // Arrange

            // Act
            var result = _controller.Update(_id, _item);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void UpdatePut_ReturnsNotFoundResult_WhenNoItemInRepositoryWithPostedId()
        {
            // Arrange
            _item.Id = _id;
            _mockFacade.Setup(mock => mock.GetById(_id)).Returns((TodoItem) null);

            // Act
            var result = _controller.Update(_id, _item);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public void UpdatePut_ReturnsOkResult_WhenUpdateSuccesfull()
        {
            // Arrange
            _item.Id = _id;
            _mockFacade.Setup(mock => mock.GetById(_id)).Returns(_item);

            // Act
            var result = _controller.Update(_id, _item);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public void Delete_ReturnsNotFoundResult_WhenItemNotFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.GetById(_id)).Returns((TodoItem)null);

            // Act
            var result = _controller.Delete(_id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public void Delete_ReturnsOkResult_WhenItemNotFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.GetById(_id)).Returns(_item);
            _mockFacade.Setup(mock => mock.Remove(_item));

            // Act
            var result = _controller.Delete(_id);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockFacade.VerifyAll();
        }
    }
}