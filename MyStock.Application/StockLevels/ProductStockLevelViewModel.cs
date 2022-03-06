namespace MyStock.Application.StockLevels
{
    public class ProductStockLevelViewModel : EntityViewModel<ProductStockLevel, ProductStockLevelViewModelValidator>
    {
        private decimal _maxQuantity;
        private decimal _minQuantity;
        private decimal _newQuantity;

        public ProductStockLevelViewModel(ProductStockLevel entity, IContext context) : base(entity, context) { }

        public decimal NetQuantity
        {
            get => _newQuantity;
            set => this.RaiseAndSetAndValidateIfChanged(ref _newQuantity, value);
        }

        public decimal MaxQuantity
        {
            get => _maxQuantity;
            set => RaiseAndSetAndValidateIfChanged(ref _maxQuantity, value, nameof(MaxQuantity), nameof(MinQuantity));
        }
        public decimal MinQuantity
        {
            get => _minQuantity;
            set => this.RaiseAndSetAndValidateIfChanged(ref _minQuantity, value, nameof(MinQuantity), nameof(MaxQuantity));
        }

        protected override void BuildCommands()
        {
            base.BuildCommands();
        }
    }
}
