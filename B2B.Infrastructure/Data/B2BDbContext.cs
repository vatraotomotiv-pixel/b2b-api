using Microsoft.EntityFrameworkCore;
using B2B.Core.Entities;

namespace B2B.Infrastructure.Data;

public class B2BDbContext : DbContext
{
    public B2BDbContext(DbContextOptions<B2BDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<ProductTranslation> ProductTranslations { get; set; }
    public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Product configuration
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.ProductCode).IsUnique();
            entity.Property(e => e.ProductCode).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.CurrencyCode).IsRequired().HasMaxLength(3);
            entity.Property(e => e.ImageUrl).HasMaxLength(500);
        });

        // ProductTranslation configuration
        modelBuilder.Entity<ProductTranslation>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasOne(e => e.Product)
                  .WithMany(p => p.Translations)
                  .HasForeignKey(e => e.ProductId)
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasIndex(e => new { e.ProductId, e.LanguageCode }).IsUnique();
            entity.Property(e => e.LanguageCode).IsRequired().HasMaxLength(5);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
        });

        // Customer configuration
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.CompanyName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.LanguageCode).IsRequired().HasMaxLength(5);
            entity.Property(e => e.CurrencyCode).IsRequired().HasMaxLength(3);
        });
    }
}
