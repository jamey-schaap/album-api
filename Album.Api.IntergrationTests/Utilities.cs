using System.Collections.Generic;
using Album.Api.RDSDb;

namespace Album.Api.IntergrationTests
{
  public class Utilities
  {
    public static void InitializeDbForTests(RDSDbContext db)
    {
      db.Albums.AddRange(GetSeedingMessages());
      db.SaveChanges();
    }

    public static void ReinitializeDbForTests(RDSDbContext db)
    {
      db.Albums.RemoveRange(db.Albums);
      InitializeDbForTests(db);
    }

    public static List<Models.Album> GetSeedingMessages()
    {
      return new List<Models.Album>()
      {
        new() { Id=1, Name=".5 The Gray Chapter", Artist="Slipknot", ImageUrl="https://picsum.photos/200/300"},
        new() { Id=2, Name="Meteora", Artist="Linkin Park", ImageUrl="https://picsum.photos/200/300"},
        new() { Id=3, Name="Hybrid Theory", Artist="Linkin Park", ImageUrl="https://picsum.photos/200/300"},
        new() { Id=4, Name="Shogun", Artist="Trivium", ImageUrl="https://picsum.photos/200/300"},
      };
    }
  }
}