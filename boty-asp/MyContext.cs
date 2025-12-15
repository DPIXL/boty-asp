using Microsoft.EntityFrameworkCore;
using boty_asp.Models;

namespace boty_asp;

public class MyContext : DbContext {
    
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Color> Colors { get; set; }
    public DbSet<Size> Sizes { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=4b1_patejdlstepan_db2;user=patejdlstepan;password=123456;Connection Timeout=30;DefaultCommandTimeout=60");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        // 1. Composite Key for ProductCategories
        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });

        // 2. Map 'SizeValue' property to 'Size' column
        modelBuilder.Entity<Size>()
            .Property(s => s.SizeValue)
            .HasColumnName("Size");
    }
}