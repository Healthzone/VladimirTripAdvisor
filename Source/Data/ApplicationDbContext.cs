using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Metadata;
using VladimirTripAdvisor.Models;

namespace VladimirTripAdvisor.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ImageModel>(entity =>
            {
                entity.Property(x => x.ImageByte).HasColumnType("longblob");
            });
            modelBuilder.Entity<ApplicationModelDB>(entity =>
            {
                entity.Property(x => x.ApplicationData).HasColumnType("JSON");
            });
        }

        public DbSet<UserModel> User { get; set; }
        public DbSet<ObjectOfVisitModel> ObjectOfVisit { get; set; }
        public DbSet<ImageModel> Image{ get; set; }
        public DbSet<ReviewModel> Review { get; set; }
        public DbSet<ApplicationModelDB> Application { get; set; }
        public DbSet<EventModel> Event { get; set; }
    }
}
