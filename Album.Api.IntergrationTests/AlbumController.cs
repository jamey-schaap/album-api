using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Album.Api.IntergrationTests
{
  public class AlbumController : IClassFixture<CustomWebApplicationFactory<Startup>>
  {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Startup> _factory;

    public AlbumController(CustomWebApplicationFactory<Startup> factory)
    {
      _factory = factory;
      _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetAlbums()
    {
      // Arrange
      var response = await _client.GetAsync($"/api/album");


      // Act
      var responseStr = await response.Content.ReadAsStringAsync();
      var albums = JsonSerializer.Deserialize<List<Models.Album>>(responseStr);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.NotNull(albums);
      Assert.Equal(4, albums.Count);
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetAlbum_GivenValidID()
    {
      // Arrange
      var id = 1;

      // Act
      var response = await _client.GetAsync($"/api/album/{id}");
      var responseStr = await response.Content.ReadAsStringAsync();
      var album = JsonSerializer.Deserialize<Models.Album>(responseStr);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.NotNull(album);
      Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task PutAlbum_GivenValidAlbumAndID()
    {
      // Arrange
      var id = 1;
      var album = new Models.Album()
      {
        Id = id,
        Name = "First Class",
        Artist = "Jack Harlow",
        ImageUrl = "https://picsum.photos/200/300"
      };

      // Act
      var jsonData = JsonSerializer.Serialize(album);
      var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
      var response = await _client.PutAsync($"/api/album/{id}", stringContent);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task PostAlbum_GivenValidAlbum()
    {
      // Arrange
      var album = new Models.Album()
      {
        Name = "First Class",
        Artist = "Jack Harlow",
        ImageUrl = "https://picsum.photos/200/300"
      };

      // Act
      var jsonData = JsonSerializer.Serialize(album);
      var stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
      var response = await _client.PostAsync("/api/album/", stringContent);

      // Assert
      response.EnsureSuccessStatusCode();
      Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
  }
}
