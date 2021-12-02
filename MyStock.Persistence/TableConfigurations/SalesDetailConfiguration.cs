namespace MyStock.Persistence.TableConfigurations;

public class SalesDetailConfiguration : BaseConfiguration<SalesDetail>
{
    public override void Configure(EntityTypeBuilder<SalesDetail> builder)
    {
        builder.Ignore(d => d.SalesDocument);
    }
}
