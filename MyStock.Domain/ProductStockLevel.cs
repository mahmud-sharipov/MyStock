namespace MyStock.Domain;

public class ProductStockLevel : EntityBase
{
    public decimal NetQuantity { get; set; }
    public decimal MaxQuantity { get; set; }
    public decimal MinQuantity { get; set; }

    public Guid ProductGuid { get; set; }
    public virtual Product Product { get; set; }

    public Guid WarehouseGuid { get; set; }
    public virtual Warehouse Warehouse { get; set; }
}
