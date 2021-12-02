namespace MyStock.Persistence.TableConfigurations;

public class PurchaseConfiguration : BaseConfiguration<Purchase>
{
    public override void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder
            .HasOne(p => p.Vendor)
            .WithMany(c => c.Purchases)
            .HasForeignKey(p => p.VendorGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
