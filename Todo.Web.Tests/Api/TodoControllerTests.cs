using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly TodoApiController _controller;
        private readonly TodoItem _item = new TodoItem();

        public TodoControllerTests()
        {
            _controller = new TodoApiController(_mockFacade.Object);
        }

        [Fact]
        public async Task GetById_ReturnsNotForundResult_WhenUserNotFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.GetByIdAsync(_id)).Returns(Task.FromResult((TodoItem)null));

            // Act
            var result = await _controller.GetById(_id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetById_ReturnsObjectResultWithObject_WhenObjectFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.GetByIdAsync(_id)).Returns(Task.FromResult(_item));

            // Act
            var result = await _controller.GetById(_id);

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
        public async Task UpdatePut_ReturnsBadRequestResult_WhenPostedItemIsNull()
        {
            // Arrange
            
            // Act
            var result = await _controller.UpdateAsync(_id, null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdatePut_ReturnsBadRequestResult_WhenIdsNotMatch()
        {
            // Arrange

            // Act
            var result = await _controller.UpdateAsync(_id, _item);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdatePut_ReturnsNotFoundResult_WhenNoItemInRepositoryWithPostedId()
        {
            // Arrange
            _item.Id = _id;
            _mockFacade.Setup(mock => mock.GetByIdAsync(_id)).Returns(Task.FromResult((TodoItem) null));

            // Act
            var result = await _controller.UpdateAsync(_id, _item);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task UpdatePut_ReturnsOkResult_WhenUpdateSuccesfull()
        {
            // Arrange
            _item.Id = _id;
            _mockFacade.Setup(mock => mock.GetByIdAsync(_id)).Returns(Task.FromResult(_item));

            // Act
            var result = await _controller.UpdateAsync(_id, _item);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task Delete_ReturnsNotFoundResult_WhenItemNotFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.GetByIdAsync(_id)).Returns(Task.FromResult((TodoItem)null));

            // Act
            var result = await _controller.DeleteAsync(_id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public async Task Delete_ReturnsOkResult_WhenItemNotFound()
        {
            // Arrange
            _mockFacade.Setup(mock => mock.GetByIdAsync(_id)).Returns(Task.FromResult(_item));
            _mockFacade.Setup(mock => mock.Remove(_item));

            // Act
            var result = await _controller.DeleteAsync(_id);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockFacade.VerifyAll();
        }

        [Fact]
        public void GetAll_ReturnsIenumerableOdTodoItems()
        {
            // Arrange
            var items = new List<TodoItem>
            {
                _item
            };
            _mockFacade.Setup(mock => mock.GetAll()).Returns(items);

            // Act
            var result = _controller.GetAll();

            // Assert
            var enumerable = Assert.IsType<List<TodoItem>>(result);
            Assert.Equal(items.Count, enumerable.Count());
        }
    }
}