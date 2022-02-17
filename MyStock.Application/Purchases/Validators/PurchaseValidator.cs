namespace MyStock.Application.Purchases.Validators
{
    public class PurchaseValidator : AbstractValidator<PurchaseViewModel>
    {
        public PurchaseValidator()
        {
            RuleFor(d => d.Vendor).NotNull();
            RuleFor(d => d.Details).NotEmpty();
            RuleForEach(d => d.Details).Must((doc, detail) => detail.IsValid);
        }
    }
}