using Microsoft.EntityFrameworkCore;

namespace WebApi.Models;
public class ShopContext : DbContext
{
    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Seed();
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}