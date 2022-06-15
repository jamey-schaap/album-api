using Microsoft.EntityFrameworkCore;

namespace Album.Api.RDSDb
{
  public class RDSDbContext : DbContext
  {
    public DbSet<Album> Albums { get; set; }
    public RDSDbContext(DbContextOptions<RDSDbContext> options) : base(options) { }
  }
}