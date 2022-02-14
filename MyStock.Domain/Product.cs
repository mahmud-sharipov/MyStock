namespace MyStock.Domain;

public class Product : EntityBase
{
    private string description;
    private string code;
    private decimal price;
    private decimal cost;
    private bool isActive;
    private Uom uom;
    private ProductCategory category;

    public Product()
    {
        StockLevels = new HashSet<ProductStockLevel>();
        DocumentDetails = new HashSet<DocumentDetail>();
    }

    public string Description { get => description; set => SetProptery(ref description, value); }
    public string Code { get => code; set => SetProptery(ref code, value); }
    public decimal Price { get => price; set => SetProptery(ref price, value); }
    public decimal Cost { get => cost; set => SetProptery(ref cost, value); }
    public bool IsActive { get => isActive; set => SetProptery(ref isActive, value); }

    public Guid UomGuid { get; set; }
    public virtual Uom Uom { get => uom; set => SetProptery(ref uom, value); }

    public Guid CategoryGuid { get; set; }
    public virtual ProductCategory Category { get => category; set => SetProptery(ref category, value); }

    public virtual ISet<ProductStockLevel> StockLevels { get; set; }
    public virtual ISet<DocumentDetail> DocumentDetails { get; set; }

    public override string ToString()
    {
        return Description;
    }
}
