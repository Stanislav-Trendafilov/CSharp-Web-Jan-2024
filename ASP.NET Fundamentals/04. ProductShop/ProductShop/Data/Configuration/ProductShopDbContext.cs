
using Microsoft.EntityFrameworkCore;
using ProductShop.Data.Models;

namespace ProductShop.Data.Configuration
{
    public class ProductShopDbContext : DbContext
    {
        public static string connectionString = @"Server=DESKTOP-9FL9J1C;Database=ShoppingList;Integrated Security=True";
  
        public ProductShopDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductNote> ProductNotes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseSqlServer(connectionString);
            }
        }
    }
}
