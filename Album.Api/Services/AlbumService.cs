using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Album.Api.RDSDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Album.Api.Services
{
  public class AlbumService : ControllerBase, IAlbumService
  {
    private readonly RDSDbContext _context;

    public AlbumService(RDSDbContext context) => _context = context;

    public async Task<IEnumerable<RDSDb.Album>> GetAlbums()
      => await _context.Albums.ToListAsync();

    public async Task<ActionResult<RDSDb.Album>> GetAlbum(int id)
    {
      RDSDb.Album album = await _context.Albums.FindAsync(id);
      if (album == null)
        return NotFound();

      return Ok(album);
    }

    public async Task<IActionResult> PutAlbum(int id, RDSDb.Album album)
    {
      if (id != album.Id)
        return BadRequest();

      _context.Entry(album).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!AlbumExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    public async Task<ActionResult<RDSDb.Album>> PostAlbum(RDSDb.Album album)
    {
      _context.Albums.Add(album);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetAlbum", new { id = album.Id }, album);
    }

    public async Task<IActionResult> DeleteAlbum(int id)
    {
      var album = await _context.Albums.FindAsync(id);
      if (album == null)
      {
        return NotFound();
      }

      _context.Albums.Remove(album);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool AlbumExists(int id)
    {
      return _context.Albums.Any(e => e.Id == id);
    }
  }
}