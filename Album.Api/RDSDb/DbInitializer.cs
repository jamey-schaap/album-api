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
          Name = "Top Gun: Maverick", 
          Artist = "Tom Cruise",
          ImageUrl = "https://loremflickr.com/320/240",
        },
        new() { 
          Name = "Pokémon: The First Movie", 
          Artist = "Ash Ketchum",
          ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/c/c9/Pokemon-mewtwo-strikes-back.jpg/220px-Pokemon-mewtwo-strikes-back.jpg",
        },
        new() { 
          Name = "Pokémon: Destiny Deoxys", 
          Artist = "Ash Ketchum",
          ImageUrl = "https://upload.wikimedia.org/wikipedia/en/thumb/7/78/Pok%C3%A9mon_Destiny_Deoxys_poster.jpg/220px-Pok%C3%A9mon_Destiny_Deoxys_poster.jpg",
        },
      };

      context.AddRange(albums);
      context.SaveChanges();
    }
  }
}