namespace MyStock.Persistence.TableConfigurations;

public class UomConfiguration : BaseConfiguration<Uom>
{
    public override void Configure(EntityTypeBuilder<Uom> builder)
    {
        base.Configure(builder);

        builder
            .HasMany(p => p.DocumentDetails)
            .WithOne(d => d.Uom)
            .HasForeignKey(p => p.UomGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(p => p.Products)
            .WithOne(d => d.Uom)
            .HasForeignKey(p => p.UomGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
