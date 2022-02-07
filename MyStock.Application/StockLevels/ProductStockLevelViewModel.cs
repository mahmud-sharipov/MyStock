namespace MyStock.Application.StockLevels
{
    public class ProductStockLevelViewModel : EntityViewModel<ProductStockLevel, ProductStockLevelViewModelValidator>, IProductStockLevelViewModel
    {
        private decimal maxQuantity;
        private decimal minQuantity;

        public ProductStockLevelViewModel(ProductStockLevel entity, IContext context) : base(entity, context) { }

        public decimal NetQuantity => Entity.NetQuantity;

        public decimal MaxQuantity
        {
            get => maxQuantity;
            set => this.RaiseAndSetAndValidateIfChanged(ref maxQuantity, value, nameof(MaxQuantity), nameof(MinQuantity));
        }
        public decimal MinQuantity
        {
            get => minQuantity;
            set => this.RaiseAndSetAndValidateIfChanged(ref minQuantity, value, nameof(MinQuantity), nameof(MaxQuantity));
        }

        protected override void BuildCommands()
        {
            base.BuildCommands();
        }
    }
}
