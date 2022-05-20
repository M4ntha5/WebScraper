using Microsoft.EntityFrameworkCore;

namespace WebScraper.Models
{
  public class TrackingContext : DbContext 
  {
    public TrackingContext() { }

    public TrackingContext(DbContextOptions<TrackingContext> options) : base(options) { }


    public DbSet<Log> Logs { get; set; }
    public DbSet<Tracking> Trackings { get; set; }
    public DbSet<TrackingImage> TrackingImages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
    }
  }
}
