namespace MyStock.Domain;

public class Warehouse : EntityBase
{
    public Warehouse()
    {
        Name = string.Empty;
        Description = string.Empty;
        StockLevels = new HashSet<ProductStockLevel>();
        DocumentDetails = new HashSet<DocumentDetail>();
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ISet<ProductStockLevel> StockLevels { get; set; }
    public virtual ISet<DocumentDetail> DocumentDetails { get; set; }
}
