using System.Collections.Generic;
using System.Threading.Tasks;

namespace Album.Api.Services
{
  public interface IAlbumService
  {
    public Task<IEnumerable<Models.Album>> GetAlbums();
    public Task<Models.Album> GetAlbum(int id);
    public Task<Result> PutAlbum(int id, Models.Album album);
    public Task<Models.Album> PostAlbum(Models.Album album);
    public Task DeleteAlbum(Models.Album album);
  }
}