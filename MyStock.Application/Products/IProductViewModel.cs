using MyStock.Application.StockLevels;

namespace MyStock.Application.Products;

public interface IProductViewModel : IEntityViewModel<Product>, IViewable
{
    string Description { get; set; }
    string Code { get; set; }
    decimal Price { get; set; }
    decimal Cost { get; set; }
    bool IsActive { get; set; }
    Uom Uom { get; set; }
    ProductCategory Category { get; set; }
    List<ProductStockLevelViewModel> StockLevels { get; set; }

}
