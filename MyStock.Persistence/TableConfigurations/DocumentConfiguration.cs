namespace MyStock.Persistence.TableConfigurations;

public class DocumentConfiguration : BaseConfiguration<Document>
{
    public override void Configure(EntityTypeBuilder<Document> builder)
    {
        base.Configure(builder);

        builder
            .HasMany(d => d.Details)
            .WithOne(d => d.Document)
            .HasForeignKey(d => d.DocumentGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(d => d.User)
            .WithMany()
            .HasForeignKey(d => d.UserGuid)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
