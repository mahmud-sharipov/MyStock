namespace MyStock.Persistence.TableConfigurations;

public class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.ToTable(typeof(TEntity).Name);
        builder.HasKey(p => p.Guid);
        builder.Property(p => p.Guid).ValueGeneratedOnAdd();
    }
}
