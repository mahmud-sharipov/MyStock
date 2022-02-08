namespace MyStock.Application.Vendors.Validators
{
    public class VendorValidator : AbstractValidator<VendorViewModel>
    {
        public VendorValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithName(Translations.FirstName);
            RuleFor(c => c.LastName).NotEmpty().WithName(Translations.LastName);
        }
    }
}