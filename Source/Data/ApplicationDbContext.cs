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
            modelBuilder.Entity<ImageModel>(entity =>
            {
                entity.Property(x => x.ImageByte).HasColumnType("longblob");
            });
            modelBuilder.Entity<ApplicationModel>(entity =>
            {
                entity.Property(x => x.ApplicationData).HasColumnType("JSON");
            });
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<ObjectOfVisitModel> ObjectOfVisit { get; set; }
        public DbSet<ImageModel> Image{ get; set; }
        public DbSet<ReviewModel> Review { get; set; }
        public DbSet<ApplicationModel> Application { get; set; }
        public DbSet<EventModel> Event { get; set; }
    }
}
