namespace MyStock.Domain;

public class Vendor : Person
{
    public Vendor()
    {
        Purchases = new HashSet<Purchase>();
    }

    public virtual ISet<Purchase> Purchases { get; set; }
}
