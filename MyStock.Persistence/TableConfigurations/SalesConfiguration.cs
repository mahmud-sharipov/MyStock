namespace MyStock.Persistence.TableConfigurations;

public class SalesConfiguration : BaseConfiguration<Sales>
{
    public override void Configure(EntityTypeBuilder<Sales> builder)
    {
        builder
            .HasOne(p => p.Customer)
            .WithMany(c => c.Sales)
            .HasForeignKey(p => p.CustomerGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
