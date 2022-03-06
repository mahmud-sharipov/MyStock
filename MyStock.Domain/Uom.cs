namespace MyStock.Domain;

public class Uom : EntityBase
{
    private string name;
    private string code;
    private string description;

    public Uom()
    {
        Name = string.Empty;
        Description = string.Empty;
        Products = new HashSet<Product>();
        Code = string.Empty;
        DocumentDetails = new HashSet<DocumentDetail>();
    }

    public string Name
    {
        get => name;
        set => SetProptery(ref name, value);
    }

    public string Code
    {
        get => code;
        set => SetProptery(ref code, value);
    }

    public string Description
    {
        get => description;
        set => SetProptery(ref description, value);
    }

    public virtual ISet<Product> Products { get; set; }
    public virtual ISet<DocumentDetail> DocumentDetails { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
