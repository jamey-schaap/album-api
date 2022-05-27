using Xunit;
using Album.Api.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Album.Api.Models;

namespace Album.Api.Tests
{
  public class HelloControllerIT
  {
    [Fact]
    public void Get_ReturnsGreet_GivenValidInput()
    {
      // Arrange
      var mockLogger = new Logger<HelloController>(new LoggerFactory());
      var controller = new HelloController(mockLogger);
      var validName = "Jamey";

      // Act
      var result = controller.Get(validName);

      // Assert
      var okResult = Assert.IsType<OkObjectResult>(result);
      var returnValue = Assert.IsType<GreetDto>(okResult.Value);
      Assert.Equal($"Hello {validName}", returnValue.greet);
    }
  }
}
