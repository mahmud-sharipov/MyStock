namespace MyStock.Persistence.TableConfigurations;

public class PurchaseDetailConfiguration : BaseConfiguration<PurchaseDetail>
{
    public override void Configure(EntityTypeBuilder<PurchaseDetail> builder)
    {
        builder.Ignore(d => d.PurchaseDocument);
    }
}
