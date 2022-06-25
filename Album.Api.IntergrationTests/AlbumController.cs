using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Collections.Generic;
using System.Text;

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

    [Fact]
    public async Task PutAlbum()
    {
      // Arrange
      var id = 1;
      var content = new RDSDb.Album()
      {
        Id = id,
        Name = "First Class",
        Artist = "Jack Harlow",
        ImageUrl = "https://picsum.photos/200/300"
      };

      // Act
      var putData = JsonSerializer.Serialize(content);
      var stringContent = new StringContent(putData, Encoding.UTF8, "application/json");
      var response = await _client.PutAsync($"/api/album/{id}", stringContent);

      // Assert
      response.EnsureSuccessStatusCode();
    }
  }
}
