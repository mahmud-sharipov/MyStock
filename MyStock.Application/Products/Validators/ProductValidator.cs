namespace MyStock.Application.Products.Validators;

public class ProductValidator : AbstractValidator<ProductViewModel>
{
    public ProductValidator()
    {
        RuleFor(x => x.Code).Cascade(CascadeMode.Stop).NotEmpty().WithName(Translations.Code);
        RuleFor(x => x.Description).Cascade(CascadeMode.Stop).NotEmpty().WithName(Translations.Description);
        RuleFor(x => x.Price).Cascade(CascadeMode.Stop).GreaterThan(0).WithName(Translations.Price);
        RuleFor(x => x.Cost).Cascade(CascadeMode.Stop).GreaterThan(0).WithName(Translations.Cost);
        RuleFor(x => x.Uom).Cascade(CascadeMode.Stop).NotNull().WithName(Translations.UOM);
        RuleFor(x => x.Category).Cascade(CascadeMode.Stop).NotNull().WithName(Translations.Category);
    }
}