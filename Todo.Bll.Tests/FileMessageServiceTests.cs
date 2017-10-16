using System.Threading.Tasks;
using Todo.Bll.Services;
using Xunit;

namespace Todo.Bll.Tests
{
    public class FileMessageServiceTests
    {
        [Fact]
        public async Task Send_CreateFileWithMessage()
        {
            // Arrange
            var messageService = new FileMessageService();

            // Act
            await messageService.Send("", "", "");

            // Assert
        }
    }
}
