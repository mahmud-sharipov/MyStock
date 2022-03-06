namespace MyStock.Domain;

public class Product : EntityBase
{
    private string _description;
    private string _code;
    private decimal _price;
    private decimal _cost;
    private bool _isActive;
    private Uom _uom;
    private ProductCategory _category;

    public Product()
    {
        StockLevels = new HashSet<ProductStockLevel>();
        DocumentDetails = new HashSet<DocumentDetail>();
    }

    public string Description { get => _description; set => SetProptery(ref _description, value); }
    public string Code { get => _code; set => SetProptery(ref _code, value); }
    public decimal Price { get => _price; set => SetProptery(ref _price, value); }
    public decimal Cost { get => _cost; set => SetProptery(ref _cost, value); }
    public bool IsActive { get => _isActive; set => SetProptery(ref _isActive, value); }

    public Guid UomGuid { get; set; }
    public virtual Uom Uom { get => _uom; set => SetProptery(ref _uom, value); }

    public Guid CategoryGuid { get; set; }
    public virtual ProductCategory Category { get => _category; set => SetProptery(ref _category, value); }

    public virtual ISet<ProductStockLevel> StockLevels { get; set; }
    public virtual ISet<DocumentDetail> DocumentDetails { get; set; }

    public override string ToString()
    {
        return Description;
    }
}
