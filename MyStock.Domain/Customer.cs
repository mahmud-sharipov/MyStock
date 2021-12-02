namespace MyStock.Domain;

public class Customer : Person
{
    public Customer()
    {
        Sales = new HashSet<Sales>();
    }

    public virtual ISet<Sales> Sales { get; set; }
}
