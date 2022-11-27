using System.Reflection.Metadata;
using VladimirTripAdvisor.Models;

namespace VladimirTripAdvisor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(x => x.ImageByte).HasColumnType("longblob");
            });
            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(x => x.ApplicationData).HasColumnType("JSON");
            });
        }

        public DbSet<User> User { get; set; }
        public DbSet<ObjectOfVisit> ObjectOfVisit { get; set; }
        public DbSet<Image> Image{ get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Event> Event { get; set; }
    }
}
