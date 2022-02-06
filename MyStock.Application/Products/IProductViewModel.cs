using MyStock.Application.Products.Pages;
using MyStock.Application.Products.Validators;

namespace MyStock.Application.Products;

public interface IProductViewModel : IEntityViewModel<Product>, IViewable
{
    string Name { get; set; }
    string Description { get; set; }
    string Code { get; set; }
    decimal Price { get; set; }
    decimal Cost { get; set; }
    bool IsActive { get; set; }
    Uom Uom { get; set; }
    ProductCategory Category { get; set; }
}

public class ProductViewModel : EntityPageViewModel<Product, ProductValidator, IProductEntityPage>, IProductViewModel
{
    private string _name;
    private string _code;
    private string _description;
    private decimal price;
    private decimal cost;
    private bool isActive;
    private Uom uom;
    private ProductCategory category;

    public ProductViewModel(Product entity, IContext context) : base(entity, context) { }

    public string Name { get => _name; set => RaiseAndSetAndValidateIfChanged(ref _name, value); }
    public string Code { get => _code; set => RaiseAndSetAndValidateIfChanged(ref _code, value); }
    public string Description { get => _description; set => RaiseAndSetIfChanged(ref _description, value); }
    public decimal Price { get => price; set => RaiseAndSetIfChanged(ref price, value); }
    public decimal Cost { get => cost; set => RaiseAndSetIfChanged(ref cost, value); }
    public bool IsActive { get => isActive; set => RaiseAndSetIfChanged(ref isActive, value); }
    public Uom Uom { get => uom; set => RaiseAndSetIfChanged(ref uom, value); }
    public ProductCategory Category { get => category; set => RaiseAndSetIfChanged(ref category, value); }

    public IEnumerable<ProductCategory> AvailableCategories { get; internal set; }
    public IEnumerable<Uom> AvailableUoms { get; internal set; }

    protected override void InitializeAssociatedProperties()
    {
        AvailableUoms = Context.Set<Uom>();
        AvailableCategories = Context.Set<ProductCategory>();
        base.InitializeAssociatedProperties();
    }
}
