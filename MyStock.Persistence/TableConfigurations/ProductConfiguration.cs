namespace MyStock.Persistence.TableConfigurations;

public class ProductConfiguration : BaseConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(p => p.Uom)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UomGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(p => p.DocumentDetails)
            .WithOne(s => s.Product)
            .HasForeignKey(s => s.ProductGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(p => p.StockLevels)
            .WithOne(s => s.Product)
            .HasForeignKey(p => p.ProductGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
