namespace MyStock.Domain;

public class Purchase : Document
{
    public Guid VendorGuid { get; set; }
    public virtual Vendor Vendor { get; set; }
}
