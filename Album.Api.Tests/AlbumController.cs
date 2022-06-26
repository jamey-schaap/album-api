using System.Linq;
using Album.Api.Controllers;
using Xunit;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Album.Api.Services;
using System.Threading.Tasks;

namespace Album.Api.Tests
{
  public class AlbumControllerUT 
  {
    [Fact]
    public async void GetAlbums()
    {
      //  Arrange
      var albums = new List<Models.Album> {
          new() { Id=1, Name=".5 The Gray Chapter", Artist="Slipknot", ImageUrl=""},
          new() { Id=2, Name="Meteora", Artist="Linkin Park", ImageUrl=""},
          new() { Id=3, Name="Hybrid Theory", Artist="Linkin Park", ImageUrl=""},
          new() { Id=4, Name="Shogun", Artist="Trivium", ImageUrl=""},
        };
      var iEnumAlbums = (IEnumerable<Models.Album>)albums;
      var albumServiceMock = new Mock<IAlbumService>();
      albumServiceMock.Setup(a => a.GetAlbums()).Returns(Task.FromResult(iEnumAlbums));
      var controller = new AlbumController(albumServiceMock.Object);

      // Act
      var response = await controller.GetAlbums();
      var okObjectResult = Assert.IsType<OkObjectResult>(response);
      var returnValue = Assert.IsType<List<Models.Album>>(okObjectResult.Value);

      // Assert
      Assert.Equal(4, returnValue.Count);
      Assert.Equal(".5 The Gray Chapter", returnValue[0].Name);
      Assert.Equal("Meteora", returnValue[1].Name);
      Assert.Equal("Hybrid Theory", returnValue[2].Name);
      Assert.Equal("Shogun", returnValue[3].Name);
    }

    [Fact]
    public async void GetAlbum_GivenValidID_ReturnsAlbum()
    {
      // Act
      var id = 1;
      var album = new Models.Album()
      {
        Id = id,
        Name = ".5 The Gray Chapter",
        Artist = "Slipknot",
        ImageUrl = ""
      };
      var albumServiceMock = new Mock<IAlbumService>();
      albumServiceMock.Setup(a => a.GetAlbum(It.IsAny<int>())).Returns(Task.FromResult(album));
      var controller = new AlbumController(albumServiceMock.Object);

      // Arrange
      var response = await controller.GetAlbum(id);
      var okObjectResult = Assert.IsType<OkObjectResult>(response);
      var returnValue = Assert.IsType<Models.Album>(okObjectResult.Value);

      // Assert
      Assert.NotNull(returnValue);
      Assert.Equal(".5 The Gray Chapter", returnValue.Name);
      Assert.Equal(id, returnValue.Id);
    }

    [Theory]
    [InlineData(-1, null)]
    [InlineData(5, null)]
    public async void ValidGetAlbumTheory(int id, Models.Album expected)
    {
      // Arrange
      var album = new Models.Album()
      {
        Id = 1,
        Name = ".5 The Gray Chapter",
        Artist = "Slipknot",
        ImageUrl = ""
      };
      var albumServiceMock = new Mock<IAlbumService>();
      albumServiceMock.Setup(a => a.GetAlbum(It.IsAny<int>())).Returns(Task.FromResult<Models.Album>(expected));
      var controller = new AlbumController(albumServiceMock.Object);

      // Act
      var response = await controller.GetAlbum(id);

      // Assert
      var notFoundResult = Assert.IsType<NotFoundResult>(response);
    }


    [Fact]
    public async void PostAlbum_GivenValidAlbum_Album()
    {
      // Arrange
      var album = new Models.Album()
      {
        Name = "First Class",
        Artist = "Jack Harlow",
        ImageUrl = ""
      };

      var albumServiceMock = new Mock<IAlbumService>();
      albumServiceMock.Setup(a => a.PostAlbum(It.IsAny<Models.Album>())).Returns(Task.FromResult(album));
      var controller = new AlbumController(albumServiceMock.Object);

      // Act
      var response = await controller.PostAlbum(album);

      // Assert
      var createdAtResult = Assert.IsType<CreatedAtActionResult>(response);
      var returnValue = Assert.IsType<Models.Album>(createdAtResult.Value);
      Assert.Equal(album, returnValue);
    }


    [Fact]
    public async void PutAlbum_GivenValidIDAlbum_Album()
    {
      // Arrange
      var albumServiceMock = new Mock<IAlbumService>();
      albumServiceMock.Setup(a => a.PutAlbum(It.IsAny<int>(), It.IsAny<Models.Album>())).Returns(Task.FromResult(Result.Ok));
      var controller = new AlbumController(albumServiceMock.Object);

      var id = 1;
      var album = new Models.Album()
      {
        Id = 1,
        Name = ".5 The Gray Chapter",
        Artist = "Slipknot",
        ImageUrl = ""
      };

      // Act
      var response = await controller.PutAlbum(id, album);

      // Assert
      var noContentResult = Assert.IsType<NoContentResult>(response);
    }

    [Fact]
    public async void DeleteAlbum_GivenAlbum()
    {
      // Arrange
      var id = 1;
      var album = new Models.Album()
      {
        Id = id,
        Name = ".5 The Gray Chapter",
        Artist = "Slipknot",
        ImageUrl = ""
      };
      var albumServiceMock = new Mock<IAlbumService>();
      albumServiceMock.Setup(a => a.GetAlbum(It.IsAny<int>())).Returns(Task.FromResult(album));
      albumServiceMock.Setup(a => a.DeleteAlbum(It.IsAny<Models.Album>()));
      var controller = new AlbumController(albumServiceMock.Object);

      // Act
      var response = await controller.DeleteAlbum(id);

      // Asert
      var noContentResult = Assert.IsType<NoContentResult>(response);
    }
  }
}