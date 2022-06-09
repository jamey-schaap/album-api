using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Album.Api.Models;
using Xunit;
using System.Net;

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
      var name = "Jamey";
      var response = await _client.GetAsync($"/api/hello?name={name}");
      response.EnsureSuccessStatusCode();
      var hostName = $"{Dns.GetHostName()} v2";

      var responseStr = await response.Content.ReadAsStringAsync();
      var responeDTO = JsonSerializer.Deserialize<GreetDto>(responseStr);

      Assert.NotNull(responeDTO);
      Assert.Equal($"Hello {name} from {hostName}", responeDTO.greet);
    }
  }
}
