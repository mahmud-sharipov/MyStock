namespace MyStock.Persistence.TableConfigurations;

public class UserConfiguration : BaseConfiguration<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(u => u.Salt)
            .HasMaxLength(40);
    }
}