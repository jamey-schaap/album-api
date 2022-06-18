using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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

    private async void SeedDatabase()
    {
      if (await context.Albums.CountAsync() == 0)
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
  }
}