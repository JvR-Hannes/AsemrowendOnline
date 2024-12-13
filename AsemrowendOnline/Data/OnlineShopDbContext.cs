using AsemrowendOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace AsemrowendOnline.Data
{
    public class OnlineShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            // Configure composite key for ProductCategory
            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Silk Scrunchie" },
                new Category { Id = 2, Name = "Cotton Scrunchie" },
                new Category { Id = 3, Name = "Pongee Scrunchie" },
                new Category { Id = 4, Name = "Printed Cotton Scrunchie" },
                new Category { Id = 5, Name = "Printed Silk Scrunchie" },
                new Category { Id = 6, Name = "Dutch Satin Scrunchie" },
                new Category { Id = 7, Name = "Bow Hair Clip" }
                );
        }

    }
}
