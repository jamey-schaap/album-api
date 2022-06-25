using System.Linq;
using Album.Api.Services;
using Xunit;

namespace Album.Api.Tests
{
  public class AlbumServiceUT : AlbumDataSeed
  {
    [Fact]
    public async void GetAlbums()
    {
      // Arrange
      var service = new AlbumService(this.context);

      // Act
      var albums = (await service.GetAlbums()).ToList();

      // Assert
      Assert.Equal(4, albums.Count);
      Assert.Equal(".5 The Gray Chapter", albums[0].Name);
      Assert.Equal("Meteora", albums[1].Name);
      Assert.Equal("Hybrid Theory", albums[2].Name);
      Assert.Equal("Shogun", albums[3].Name);
    }

    [Fact]
    public async void GetAlbum_GivenValidID_ReturnsAlbum()
    {
      // Arrange
      var service = new AlbumService(this.context);

      // Act
      var album = await service.GetAlbum(2);

      // Assert
      Assert.NotNull(album);
      Assert.Equal(2, album.Id);
      Assert.Equal("Meteora", album.Name);
    }

    [Theory]
    [InlineData(-1, null)]
    [InlineData(5, null)]
    public async void ValidGetAlbumTheory(int id, RDSDb.Album expected)
    {
      // Arrange
      var service = new AlbumService(this.context);

      // Act
      var album = await service.GetAlbum(id);

      // Assert
      Assert.Equal(expected, album);
    }


    [Fact]
    public async void PostAlbum_GivenValidAlbum_Album()
    {
      // Arrange
      var service = new AlbumService(this.context);

      var id = 6;
      var album = new RDSDb.Album()
      {
        Id = id,
        Name = "First Class",
        Artist = "Jack Harlow",
        ImageUrl = ""
      };

      // Act
      var responseAlbum = await service.PostAlbum(album);

      // Assert
      Assert.Equal(album, responseAlbum);
      Assert.Equal(album, this.context.Albums.Find(id));
    }


    [Fact]
    public async void PutAlbum_GivenValidIDAlbum_Album()
    {
      // Arrange
      var service = new AlbumService(this.context);

      var id = 1;
      var album = await service.GetAlbum(id);
      var newAlbumName = "Bleed The Future";
      album.Name = newAlbumName;

      // Act
      var response = await service.PutAlbum(id, album);

      // Assert
      Assert.Equal(Result.Ok, response);
      var modifiedAlbum = this.context.Albums.Find(id);
      Assert.Equal(newAlbumName, modifiedAlbum.Name);
    }

    [Fact]
    public async void DeleteAlbum_GivenAlbum()
    {
      // Arrange
      var service = new AlbumService(this.context);
      var id = 1;
      var album = await service.GetAlbum(id);

      // Act
      await service.DeleteAlbum(album);
      var response = this.context.Albums.Find(id);

      // Assert
      Assert.Null(response);
    }
  }
}

