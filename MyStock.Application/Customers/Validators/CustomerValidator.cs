namespace MyStock.Application.Customers.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty().WithName(Translations.FirstName);
            RuleFor(c => c.LastName).NotEmpty().WithName(Translations.LastName);
        }
    }
}