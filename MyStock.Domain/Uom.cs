namespace MyStock.Domain;

public class Uom : EntityBase
{
    public Uom()
    {
        Name = string.Empty;
        Description = string.Empty;
        Products = new HashSet<Product>();
        DocumentDetails = new HashSet<DocumentDetail>();
    }

    public string Name { get; set; }
    public string Description { get; set; }

    public virtual ISet<Product> Products { get; set; }
    public virtual ISet<DocumentDetail> DocumentDetails { get; set; }
}
