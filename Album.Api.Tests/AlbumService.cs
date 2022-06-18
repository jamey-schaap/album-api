using System.Collections.Generic;
using System.Linq;
using Album.Api.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Album.Api.RDSDb;
using System;

namespace Album.Api.Tests
{
  public abstract class TestBase : IDisposable
  {
    protected static DbContextOptions<RDSDbContext> dbContextOptions = new DbContextOptionsBuilder<RDSDbContext>()
      .UseInMemoryDatabase(databaseName: "AlbumServiceUT")
      .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
      .Options;

    protected RDSDbContext context;

    // Called before every test method.
    protected TestBase() => Setup();

    public void Setup()
    {
      context = new RDSDbContext(dbContextOptions);
      context.Database.EnsureCreated();

      SeedDatabase();
    }

    // Called after every test method.
    public void Dispose() => context.Database.EnsureDeleted();

    private void SeedDatabase()
    {
      var albums = new List<RDSDb.Album> {
        new() { Id=1, Name=".5 The Gray Chapter", Artist="Slipknot", ImageUrl=""},
        new() { Id=2, Name="Meteora", Artist="Linkin Park", ImageUrl=""},
        new() { Id=3, Name="Hybrid Theory", Artist="Linkin Park", ImageUrl=""},
        new() { Id=4, Name="Shogun", Artist="Trivium", ImageUrl=""},
      };
      context.Albums.AddRange(albums);
      context.SaveChanges();
    }
  }

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

