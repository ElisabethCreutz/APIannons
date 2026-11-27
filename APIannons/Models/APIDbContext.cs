using Microsoft.EntityFrameworkCore;

namespace APIannons.Models
{
    public class APIDbContext:DbContext
    {
        public APIDbContext(DbContextOptions<APIDbContext> options) : base(options) { }
        public DbSet<Ad> Ads { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ad>()
                .HasOne(ad => ad.Category)
                .WithMany()
                .HasForeignKey(ad => ad.CategoryId);
        }
    }
}
