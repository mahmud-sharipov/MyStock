namespace MyStock.Application.Documents.Validators;

public class DocumentDetailValidator : AbstractValidator<DocumentDetailViewModel>
{
    public DocumentDetailValidator()
    {
        RuleFor(d => d.Quantity).NotEmpty().WithName(Translations.Quantity);
        RuleFor(d => d.UnitPrice).NotEmpty().WithName(Translations.UnitPrice);
        RuleFor(d => d.Product).NotEmpty().WithName(Translations.Product);
        RuleFor(d => d.Uom).NotEmpty().WithName(Translations.UOM);
        RuleFor(d => d.Warehouse).NotEmpty().WithName(Translations.Warehouse);
    }
}
