namespace MyStock.Application.StockLevels
{
    public class ProductStockLevelViewModelValidator : AbstractValidator<ProductStockLevelViewModel>
    {
        public ProductStockLevelViewModelValidator()
        {
            RuleFor(x => x.MaxQuantity).Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0)
                .GreaterThanOrEqualTo(p => p.MinQuantity)
                .WithName(Translations.MaxQuantity);
            RuleFor(x => x.MinQuantity).Cascade(CascadeMode.Stop)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(p => p.MaxQuantity)
                .WithName(Translations.MinQuantity);
        }
    }
}
