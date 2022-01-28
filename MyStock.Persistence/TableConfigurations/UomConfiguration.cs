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

        builder.HasData(new[]
        {
            new Uom() { Name = "Штук", Code = "шт" },
            new Uom() { Name = "Бутылка", Code = "бтл" },
            new Uom() { Name = "Коробка", Code = "Коробка" },
            new Uom() { Name = "Ящик", Code = "Ящик" },
            new Uom() { Name = "Квадратный миллиметр", Code = "мм2" },
            new Uom() { Name = "Квадратный сантиметр", Code = "см2" },
            new Uom() { Name = "Квадратный метр", Code = "м2" },
            new Uom() { Name = "Сантиметр", Code = "см" },
            new Uom() { Name = "Дециметр", Code = "дм" },
            new Uom() { Name = "Километр", Code = "км" },
            new Uom() { Name = "Метр", Code = "м" },
            new Uom() { Name = "Миллиметр", Code = "мм" },
            new Uom() { Name = "Грамм", Code = "г" },
            new Uom() { Name = "Килограмм", Code = "кг" },
            new Uom() { Name = "Тонна", Code = "т" },
            new Uom() { Name = "Кубический сантиметр", Code = "см3" },
            new Uom() { Name = "Кубический дециметр", Code = "дм3" },
            new Uom() { Name = "Кубический метр", Code = "м3" },
            new Uom() { Name = "Кубический миллиметр", Code = "мм3" },
            new Uom() { Name = "Литр", Code = "л" },
            new Uom() { Name = "Миллилитр", Code = "мм" },
        });
    }
}
