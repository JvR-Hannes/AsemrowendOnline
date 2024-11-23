using AsemrowendOnline.Models;
using Microsoft.EntityFrameworkCore;

namespace AsemrowendOnline.Data
{
    public class OnlineShopDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : base(options) { }
    }
}
