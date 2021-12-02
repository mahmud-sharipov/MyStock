namespace MyStock.Persistence.TableConfigurations;

public class CustomerConfiguration : BaseConfiguration<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .HasMany(c=>c.Sales)
            .WithOne(s=>s.Customer)
            .HasForeignKey(s=>s.CustomerGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
