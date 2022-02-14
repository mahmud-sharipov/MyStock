namespace MyStock.Application.Documents.Validators
{
    public class DocumentDetailValidator : AbstractValidator<DocumentDetailViewModel>
    {
        public DocumentDetailValidator()
        {
            RuleFor(d => d.Quantity).NotEmpty().WithName(Translations.Quantity);
        }
    }
}