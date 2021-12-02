namespace MyStock.Persistence.TableConfigurations;

public class VendorConfiguration : BaseConfiguration<Vendor>
{
    public override void Configure(EntityTypeBuilder<Vendor> builder)
    {
        builder
            .HasMany(c => c.Purchases)
            .WithOne(s => s.Vendor)
            .HasForeignKey(s => s.VendorGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
