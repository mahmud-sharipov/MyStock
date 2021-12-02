namespace MyStock.Persistence.TableConfigurations;

public class ProductStockLevelConfiguration : BaseConfiguration<ProductStockLevel>
{
    public override void Configure(EntityTypeBuilder<ProductStockLevel> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(p => p.Product)
            .WithMany(c => c.StockLevels)
            .HasForeignKey(p => p.ProductGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(p => p.Warehouse)
            .WithMany(c => c.StockLevels)
            .HasForeignKey(p => p.WarehouseGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
