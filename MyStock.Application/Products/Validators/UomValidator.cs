using MyStock.Application.Products;

namespace MyStock.Application.Products.Validators;

public class ProductValidator : AbstractValidator<ProductViewModel>
{
    public ProductValidator()
    {
        
        RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().WithName(Translations.Name);
        RuleFor(x => x.Code).Cascade(CascadeMode.Stop).NotEmpty().WithName(Translations.Code);
    }
}
