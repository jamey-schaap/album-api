using System.Collections.Generic;
using System.Linq;

namespace Album.Api.RDSDb
{
  class DbInitializer
  {
    public static void Initialize(RDSDbContext context)
    {
      context.Database.EnsureCreated();

      if (context.Albums.Any())
        return;

      List<Models.Album> albums = new List<Models.Album> {
        new() { 
          Name = "Zavali Ebalo", 
          Artist = "Slaughter To Prevail",
          ImageUrl = "https://i1.sndcdn.com/artworks-dvaQQH6JZARF-0-t500x500.jpg",
        },
        new() { 
          Name = "Meteora", 
          Artist = "Linkin Park",
          ImageUrl = "https://www.bol.com/nl/nl/p/meteora/1000004004289106/",
        },
        new() { 
          Name = "What The Dead Man Say", 
          Artist = "Trivium",
          ImageUrl = "https://www.amazon.nl/Trivium-What-Dead-Men-Say/dp/B0851LYSHT",
        },
      };

      context.AddRange(albums);
      context.SaveChanges();
    }
  }
}