using System.Net;
using Album.Api.Services;
using Xunit;

namespace Album.Api.Tests
{
  public class GreetingServiceUT
  {
    [Fact]
    public void Greet_ReturnsGreet_GivenValidName()
    {
      // Arrange
      var validName = "Jamey";
      var hostName = $"{Dns.GetHostName()} v2";

      // Act
      var result = GreetingService.Greet(validName);

      // Assert
      Assert.NotNull(result);
      Assert.Equal($"Hello {validName} from {hostName}", result);
    }

    [Theory]
    [InlineData(null, "Hello World")]
    [InlineData("", "Hello World")]
    [InlineData(" ", "Hello World")]
    public void ValidGreetingTheory(string name, string expected)
    {
      // Arrange
      var hostName = $"{Dns.GetHostName()} v2";

      // Act
      var result = GreetingService.Greet(name);

      // Assert
      Assert.NotNull(result);
      Assert.Equal($"{expected} from {hostName}", result);
    }
  }
}
