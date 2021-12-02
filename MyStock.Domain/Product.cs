namespace MyStock.Domain;

public class Product : EntityBase
{
    public Product()
    {
        StockLevels = new HashSet<ProductStockLevel>();
        DocumentDetails = new HashSet<DocumentDetail>();
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public bool IsActive { get; set; }

    public Guid UomGuid { get; set; }
    public virtual Uom Uom { get; set; }

    public Guid CategoryGuid { get; set; }
    public virtual ProductCategory Category { get; set; }

    public virtual ISet<ProductStockLevel> StockLevels { get; set; }
    public virtual ISet<DocumentDetail> DocumentDetails { get; set; }
}
