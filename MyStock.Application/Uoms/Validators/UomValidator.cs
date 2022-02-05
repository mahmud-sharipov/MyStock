namespace MyStock.Application.Uoms.Validators;

public class UomValidator : AbstractValidator<UomViewModel>
{
    public UomValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Code).NotEmpty();
    }
}
