using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Album.Api.IntergrationTests
{
  public class HealthController
  {
    private readonly HttpClient _client;
    public HealthController()
    {
      var appFactory = new WebApplicationFactory<Startup>();
      _client = appFactory.CreateClient();
    }
    
    [Fact]
    public async Task GetHealth()
    {
      var response = await _client.GetAsync("/health");
      response.EnsureSuccessStatusCode();

      var responseStr = await response.Content.ReadAsStringAsync();

      Assert.NotNull(responseStr);
      Assert.Equal("health", responseStr);
    }
  }
}
