using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Album.Api.RDSDb;
using Album.Api.Services;

namespace Album.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AlbumController : ControllerBase
  {
    private readonly IAlbumService _albumService;

    public AlbumController(RDSDbContext context)
      => _albumService = new AlbumService(context);

    /// GET: api/Album
    [HttpGet]
    public async Task<ActionResult<IEnumerable<RDSDb.Album>>> GetAlbums()
      => Ok(await _albumService.GetAlbums());

    // GET: api/Album/5
    [HttpGet("{id}")]
    public async Task<ActionResult<RDSDb.Album>> GetAlbum(int id)
    {
      RDSDb.Album album = await _albumService.GetAlbum(id);
      if (album == null)
        return NotFound();
      return Ok(album);
    }

    // PUT: api/Album/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAlbum(int id, RDSDb.Album album)
    {
      if (id != album.Id)
        return BadRequest();

      Result result = await _albumService.PutAlbum(id, album);
      switch (result)
      {
        case Result.Err:
          return NotFound();

        case Result.Ok:
          goto default;

        default:
          return NoContent();
      }

    }

    // POST: api/Album
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<RDSDb.Album>> PostAlbum(RDSDb.Album album)
    {
      RDSDb.Album newAlbum = await _albumService.PostAlbum(album);
      return CreatedAtAction("GetAlbum", new { id = album.Id }, album);
    }

    // DELETE: api/Album/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAlbum(int id)
    {
      var album = await _albumService.GetAlbum(id);
      if (album == null)
      {
        return NotFound();
      }

      await _albumService.DeleteAlbum(album);

      return NoContent();
    }
  }
}
