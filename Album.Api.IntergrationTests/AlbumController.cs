using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Collections.Generic;

namespace Album.Api.IntergrationTests
{
  public class AlbumController : IClassFixture<CustomWebApplicationFactory<Startup>>
  {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public AlbumController(CustomWebApplicationFactory<Startup> factory)
    {
      // var appFactory = new CustomWebApplicationFactory<Startup>();
      _factory = factory;
      _client = factory.CreateClient();
    }


    [Fact]
    public async Task GetAlbums()
    {
      var response = await _client.GetAsync($"/api/album");
      response.EnsureSuccessStatusCode();

      var responseStr = await response.Content.ReadAsStringAsync();
      var albums = JsonSerializer.Deserialize<List<RDSDb.Album>>(responseStr);

      Assert.NotNull(albums);
      Assert.Equal(4, albums.Count);
    }

    [Fact]
    public async Task GetAlbum_GivenValidID()
    {
      var id = 1;
      var response = await _client.GetAsync($"/api/album/{id}");
      response.EnsureSuccessStatusCode();

      var responseStr = await response.Content.ReadAsStringAsync();
      var album = JsonSerializer.Deserialize<RDSDb.Album>(responseStr);

      Assert.NotNull(album);
    }
  }
}
