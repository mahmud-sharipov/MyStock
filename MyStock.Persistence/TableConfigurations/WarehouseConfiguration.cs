namespace MyStock.Persistence.TableConfigurations;

public class WarehouseConfiguration : BaseConfiguration<Warehouse>
{
    public override void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        base.Configure(builder);

        builder
            .HasMany(c => c.DocumentDetails)
            .WithOne(s => s.Warehouse)
            .HasForeignKey(s => s.WarehouseGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(c => c.StockLevels)
            .WithOne(s => s.Warehouse)
            .HasForeignKey(s => s.WarehouseGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
