namespace MyStock.Application.Sale.Validators
{
    public class SalesValidator : AbstractValidator<SalesViewModel>
    {
        public SalesValidator()
        {
            RuleFor(d => d.Customer).NotNull();
            RuleFor(d => d.Details).NotEmpty();
            RuleForEach(d => d.Details).Must((doc, detail) => detail.IsValid);
        }
    }
}