using MyStock.Application.Category;
using MyStock.Application.Products.Pages;
using MyStock.Application.Products.Validators;
using MyStock.Application.StockLevels;
using MyStock.Application.Uoms;

namespace MyStock.Application.Products;

public class ProductViewModel : EntityPageViewModel<Product, ProductValidator, IProductEntityPage>
{
    private string _code;
    private string _description;
    private decimal price;
    private decimal cost;
    private bool isActive;
    private Uom uom;
    private ProductCategory category;

    public ProductViewModel(Product entity, IContext context) : base(entity, context)
    {
    }

    public string Code { get => _code; set => RaiseAndSetAndValidateIfChanged(ref _code, value); }
    public string Description { get => _description; set => RaiseAndSetAndValidateIfChanged(ref _description, value); }
    public decimal Price { get => price; set => RaiseAndSetAndValidateIfChanged(ref price, value); }
    public decimal Cost { get => cost; set => RaiseAndSetAndValidateIfChanged(ref cost, value); }
    public bool IsActive { get => isActive; set => RaiseAndSetIfChanged(ref isActive, value); }
    public Uom Uom { get => uom; set => RaiseAndSetAndValidateIfChanged(ref uom, value); }
    public ProductCategory Category { get => category; set => RaiseAndSetAndValidateIfChanged(ref category, value); }
    public List<ProductStockLevelViewModel> StockLevels { get; set; }
    public ProductStockLevelViewModel StockLevel => StockLevels.FirstOrDefault();

    public ProductCategorySearchViewModel CategorySearchViewModel => new ProductCategorySearchViewModel(Context, c => Category = c)
    {
        SelectedSearchItem = Entity.Category,
        SearchText = Entity.Category?.Title ?? ""
    };

    public UomSearchViewModel UomSearchViewModel => new UomSearchViewModel(Context, c => Uom = c)
    {
        SelectedSearchItem = Entity.Uom,
        SearchText = Entity.Uom?.Title ?? ""
    };

    protected override void InitializeAssociatedProperties()
    {
        StockLevels = new List<ProductStockLevelViewModel>();
        foreach (var stockLevel in Entity.StockLevels)
            StockLevels.Add(new ProductStockLevelViewModel(stockLevel, Context));

        base.InitializeAssociatedProperties();
    }

    public override bool ValidateBeforeSaveChange()
    {
        return base.ValidateBeforeSaveChange() && StockLevels.All(s => s.ValidateBeforeSaveChange());
    }

    public override void ActionBeforeSave()
    {
        base.ActionBeforeSave();
        StockLevels.ForEach(s => s.ActionBeforeSave());
    }
}
