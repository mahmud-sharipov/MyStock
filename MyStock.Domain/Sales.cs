namespace MyStock.Domain;

public class Sales : Document
{
    private Customer customer;

    public Guid CustomerGuid { get; set; }
    public virtual Customer Customer { get => customer; set => SetProptery(ref customer, value); }
}
