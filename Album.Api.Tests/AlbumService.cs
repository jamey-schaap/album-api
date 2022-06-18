using System.Linq;
using Album.Api.Services;
using Xunit;

namespace Album.Api.Tests
{
  public class AlbumServiceUT : TestBase
  {

    [Fact]
    public async void GetAlbums()
    {
      var service = new AlbumService(this.context);
      var albums = (await service.GetAlbums()).ToList();

      Assert.Equal(4, albums.Count);
      Assert.Equal(".5 The Gray Chapter", albums[0].Name);
      Assert.Equal("Meteora", albums[1].Name);
      Assert.Equal("Hybrid Theory", albums[2].Name);
      Assert.Equal("Shogun", albums[3].Name);
    } 

    [Fact]
    public async void GetAlbum_GivenValidID_ReturnsAlbum()
    {
      var service = new AlbumService(this.context);
      var album = await service.GetAlbum(2);

      Assert.NotNull(album);
      Assert.Equal(2, album.Id);
      Assert.Equal("Meteora", album.Name);
    }

    [Theory]
    [InlineData(-1, null)]
    [InlineData(5, null)]
    public async void ValidGetAlbumTheory(int id, RDSDb.Album expected)
    {
      var service = new AlbumService(this.context);
      var album = await service.GetAlbum(id);

      Assert.Equal(expected, album);
    }


    [Fact]
    public async void PostAlbum_GivenValidAlbum_Album()
    {
      var service = new AlbumService(this.context);

      var id = 6;
      var album = new RDSDb.Album()
      {
        Id = id,
        Name = "First Class",
        Artist = "Jack Harlow",
        ImageUrl = ""
      };

      var responseAlbum = await service.PostAlbum(album);

      Assert.Equal(album, responseAlbum);
      Assert.Equal(album, context.Albums.Find(id));
    }


    // [Fact]
    // public async void PutAlbum_GivenValidIDAlbum_Album()
    // {
    //   var service = new AlbumService(this.context);

    //   var id = 1;
    //   var album = new RDSDb.Album()
    //   {
    //     Id = 1,
    //     Name = ".5 The Gray Chapter",
    //     Artist = "Slipknot",
    //     ImageUrl = ""
    //   };

    //   var response = await service.PutAlbum(album);

    //   Assert.Equal(Result.Ok, responseAlbum);
    //   Assert.Equal(album, context.Albums.Find(id));
    // }

    // [Fact]
    // public async void DeleteAlbum_GivenAlbum()
    // {
    //   var service = new AlbumService(this.context);
    //   var album = new RDSDb.Album()
    //   {
    //     Id = 1,
    //     Name = ".5 The Gray Chapter",
    //     Artist = "Slipknot",
    //     ImageUrl = ""
    //   };

    //   await service.DeleteAlbum(album);
    //   Assert.Equal(null, context.Albums.Find(album));
    // }
  }
}

