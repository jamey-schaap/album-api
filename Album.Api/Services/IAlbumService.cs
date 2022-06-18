using System.Collections.Generic;
using System.Threading.Tasks;

namespace Album.Api.Services
{
  internal interface IAlbumService
  {
    public Task<IEnumerable<RDSDb.Album>> GetAlbums();
    public Task<RDSDb.Album> GetAlbum(int id);
    public Task<Result> PutAlbum(int id, RDSDb.Album album);
    public Task<RDSDb.Album> PostAlbum(RDSDb.Album album);
    public Task DeleteAlbum(RDSDb.Album album);
  }
}