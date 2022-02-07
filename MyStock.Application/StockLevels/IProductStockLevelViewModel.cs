namespace MyStock.Application.StockLevels
{
    public interface IProductStockLevelViewModel : IEntityViewModel<ProductStockLevel>
    {
        decimal NetQuantity { get; }
        decimal MaxQuantity { get; set; }
        decimal MinQuantity { get; set; }
    }
}
