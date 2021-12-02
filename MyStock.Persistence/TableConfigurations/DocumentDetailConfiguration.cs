namespace MyStock.Persistence.TableConfigurations;

public class DocumentDetailConfiguration : BaseConfiguration<DocumentDetail>
{
    public override void Configure(EntityTypeBuilder<DocumentDetail> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(d => d.Document)
            .WithMany(p => p.Details)
            .HasForeignKey(s => s.DocumentGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(d => d.Product)
            .WithMany(p => p.DocumentDetails)
            .HasForeignKey(s => s.ProductGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(d => d.Warehouse)
            .WithMany(p => p.DocumentDetails)
            .HasForeignKey(s => s.WarehouseGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(d => d.Uom)
            .WithMany(p => p.DocumentDetails)
            .HasForeignKey(s => s.UomGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

    }
}
