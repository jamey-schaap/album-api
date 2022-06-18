using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Api.RDSDb;
using Microsoft.EntityFrameworkCore;


namespace Album.Api.Services
{
  public class AlbumService : IAlbumService
  {
    private readonly RDSDbContext _context;

    public AlbumService(RDSDbContext context) => _context = context;

    public async Task<IEnumerable<RDSDb.Album>> GetAlbums()
      => await _context.Albums.ToListAsync();

    public async Task<RDSDb.Album> GetAlbum(int id)
      => await _context.Albums.FindAsync(id);

    public async Task<Result> PutAlbum(int id, RDSDb.Album album)
    {

      _context.Entry(album).State = EntityState.Modified;
      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AlbumExists(id))
        {
          return Result.Err;
        }
        else
        {
          throw;
        }
      }

      return Result.Ok;
    }

    public async Task<RDSDb.Album> PostAlbum(RDSDb.Album album)
    {
      _context.Albums.Add(album);
      await _context.SaveChangesAsync();
      return album;
    }
  
    public async Task DeleteAlbum(RDSDb.Album album)
    {
      _context.Albums.Remove(album);
      await _context.SaveChangesAsync();
    }

    private bool AlbumExists(int id)
      => _context.Albums.Any(e => e.Id == id);
  }
}