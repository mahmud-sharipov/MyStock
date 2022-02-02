namespace MyStock.Core;

public static class IValidatorExtensions
{
    public static ValidationResult Validate(this IValidator validator, IViewModel viewModel) =>
        validator.Validate(new ValidationContext<object>(viewModel));
}


