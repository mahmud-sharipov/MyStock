namespace MyStock.Domain;

public class Sales : Document
{
    public Guid CustomerGuid { get; set; }
    public virtual Customer Customer { get; set; }
}
