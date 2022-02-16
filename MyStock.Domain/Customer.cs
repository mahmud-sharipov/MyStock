namespace MyStock.Domain;

public class Customer : Person
{
    public static readonly Guid GeneralCustomerGuid = new Guid("6A86A7B3-F054-420D-8763-CB3454A4D38F");

    public Customer()
    {
        Sales = new HashSet<Sales>();
    }

    public bool IsGeneral { get; set; }

    public virtual ISet<Sales> Sales { get; set; }
}
