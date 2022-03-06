namespace MyStock.Application.Category;

public class ProductCategoryValidator : AbstractValidator<ProductCategoryViewModel>
{
    public ProductCategoryValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithName(Translations.Name);
    }
}
