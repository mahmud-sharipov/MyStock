namespace MyStock.Domain;

public class Document : EntityBase
{
    private DateTime date;
    private string description;
    private decimal discount;

    public Document()
    {
        Details = new HashSet<DocumentDetail>();
    }

    public DateTime Date { get => date; set => SetProptery(ref date, value); }
    public string Description { get => description; set => SetProptery(ref description, value); }
    public decimal Discount { get => discount; set => SetProptery(ref discount, value); }

    public virtual ISet<DocumentDetail> Details { get; set; }
}
