namespace MyStock.Persistence.TableConfigurations;

public class ProductCategoryConfiguration : BaseConfiguration<ProductCategory>
{
    public override void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        base.Configure(builder);

        builder
            .HasOne(c => c.Parent)
            .WithMany(c => c.ChildCategories)
            .HasForeignKey(c => c.ParentGuid)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(c => c.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryGuid)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}
