namespace MyStock.Domain;

public class DocumentDetail : EntityBase
{
    public decimal Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string Description { get; set; }

    public Guid DocumentGuid { get; set; }
    public virtual Document Document { get; set; }

    public Guid ProductGuid { get; set; }
    public virtual Product Product { get; set; }

    public Guid UomGuid { get; set; }
    public virtual Uom Uom { get; set; }

    public Guid WarehouseGuid { get; set; }
    public virtual Warehouse Warehouse { get; set; }
}
