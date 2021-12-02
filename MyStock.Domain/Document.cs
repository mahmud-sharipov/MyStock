namespace MyStock.Domain;

public class Document : EntityBase
{
    public Document()
    {
        Details = new HashSet<DocumentDetail>();
    }

    public DateTime Date { get; set; }
    public string Description { get; set; }
    public decimal Discount { get; set; }

    public virtual ISet<DocumentDetail> Details { get; set; }

}
