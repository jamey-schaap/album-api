using System.Linq;
using Album.Api.Controllers;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Album.Api.Tests
{
  public class AlbumControllerUT : TestBase
  {
    [Fact]
    public async void GetAlbums()
    {
      var controller = new AlbumController(this.context);
      var response = await controller.GetAlbums();

      var okObjectResult = Assert.IsType<OkObjectResult>(response);
      var returnValue = Assert.IsType<List<RDSDb.Album>>(okObjectResult.Value);

      Assert.Equal(4, returnValue.Count);
      Assert.Equal(".5 The Gray Chapter", returnValue[0].Name);
      Assert.Equal("Meteora", returnValue[1].Name);
      Assert.Equal("Hybrid Theory", returnValue[2].Name);
      Assert.Equal("Shogun", returnValue[3].Name);
    }

    [Fact]
    public async void GetAlbum_GivenValidID_ReturnsAlbum()
    {
      var controller = new AlbumController(this.context);
      var id = 1;
      var response = await controller.GetAlbum(id);

      var okObjectResult = Assert.IsType<OkObjectResult>(response);
      var returnValue = Assert.IsType<RDSDb.Album>(okObjectResult.Value);

      Assert.NotNull(returnValue);
      Assert.Equal(".5 The Gray Chapter", returnValue.Name);
      Assert.Equal(id, returnValue.Id);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(5)]
    public async void ValidGetAlbumTheory(int id)
    {
      var controller = new AlbumController(this.context);
      var response = await controller.GetAlbum(id);

      var result = Assert.IsType<NotFoundResult>(response);
    }


    // [Fact]
    // public async void PostAlbum_GivenValidAlbum_Album()
    // {
    //   var controller = new AlbumController(this.context);

    //   var id = 6;
    //   var album = new RDSDb.Album()
    //   {
    //     Id = id,
    //     Name = "First Class",
    //     Artist = "Jack Harlow",
    //     ImageUrl = ""
    //   };

    //   var response = await controller.PostAlbum(album);

    //   var okObjectResult = Assert.IsType<CreatedAtActionResult>(response);
    //   var returnValue = Assert.IsType<RDSDb.Album>(okObjectResult.Value);
    //   Assert.Equal(album, returnValue);
    // }


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