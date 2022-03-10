namespace MyStock.Domain;

public class Vendor : Person
{
    public static readonly Guid AnonymousVendorGuid = new Guid("7CE28A56-E039-412C-AE0C-23D7CF13D7C9");

    public Vendor()
    {
        Purchases = new HashSet<Purchase>();
    }

    public bool IsAnonymous { get; set; }

    public virtual ISet<Purchase> Purchases { get; set; }

}
