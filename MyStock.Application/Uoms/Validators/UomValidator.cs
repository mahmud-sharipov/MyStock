namespace MyStock.Application.Uoms.Validators;

public class UomValidator : AbstractValidator<UomViewModel>
{
    public UomValidator()
    {
        
        RuleFor(x => x.Name).Cascade(CascadeMode.Stop).NotEmpty().MinimumLength(10).WithName(Translations.Name);
        RuleFor(x => x.Code).Cascade(CascadeMode.Stop).NotEmpty().WithName(Translations.Code);
    }
}
