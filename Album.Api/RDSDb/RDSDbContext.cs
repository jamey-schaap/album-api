using Microsoft.EntityFrameworkCore;

namespace Album.Api.RDSDb
{
  public class RDSDbContext : DbContext
  {
    public virtual DbSet<Models.Album> Albums { get; set; }
    public RDSDbContext(DbContextOptions<RDSDbContext> options) : base(options) { }
    public RDSDbContext() { }
  }
}