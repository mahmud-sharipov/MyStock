namespace MyStock.Domain;

public class DocumentDetail : EntityBase
{
    private decimal quantity;
    private decimal unitPrice;
    private string description;
    private Product product;
    private Uom uom;
    private Warehouse warehouse;

    public decimal Quantity
    {
        get => quantity;
        set
        {
            SetProptery(ref quantity, value);
            Document?.RaisePropertyChanged(nameof(Document.Subtotal));
        }
    }
    public decimal UnitPrice
    {
        get => unitPrice; set
        {
            SetProptery(ref unitPrice, value);
            Document?.RaisePropertyChanged(nameof(Document.Subtotal));
        }
    }
    public string Description { get => description; set => SetProptery(ref description, value); }

    public Guid DocumentGuid { get; set; }
    public virtual Document Document { get; set; }

    public Guid ProductGuid { get; set; }
    public virtual Product Product { get => product; set => SetProptery(ref product, value); }

    public Guid UomGuid { get; set; }
    public virtual Uom Uom
    {
        get => uom;
        set
        {
            SetProptery(ref uom, value);
        }
    }

    public Guid WarehouseGuid { get; set; }
    public virtual Warehouse Warehouse { get => warehouse; set => SetProptery(ref warehouse, value); }
}
