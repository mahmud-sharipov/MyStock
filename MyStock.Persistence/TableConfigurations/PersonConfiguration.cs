namespace MyStock.Persistence.TableConfigurations;

public class PersonConfiguration : BaseConfiguration<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);
    }
}
