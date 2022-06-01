using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Album.Api.Models;
using Xunit;

namespace Album.Api.IntergrationTests
{
  public class HelloController
  {
    private readonly HttpClient _client;
    public HelloController()
    {
      var appFactory = new WebApplicationFactory<Startup>();
      _client = appFactory.CreateClient();
    }
    
    [Fact]
    public async Task GetGreeting()
    {
      var response = await _client.GetAsync("/api/hello?name=Jamey");
      response.EnsureSuccessStatusCode();

      var responseStr = await response.Content.ReadAsStringAsync();
      var responeDTO = JsonSerializer.Deserialize<GreetDto>(responseStr);

      Assert.NotNull(responeDTO);
      Assert.Equal("Hello Jamey", responeDTO.greet);
    }
  }
}
