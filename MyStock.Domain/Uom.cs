namespace MyStock.Domain;

public class Uom : EntityBase
{
    private string _name;
    private string _code;
    private string _description;

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
        get => _name;
        set => SetProptery(ref _name, value);
    }

    public string Code
    {
        get => _code;
        set => SetProptery(ref _code, value);
    }

    public string Description
    {
        get => _description;
        set => SetProptery(ref _description, value);
    }

    public virtual ISet<Product> Products { get; set; }
    public virtual ISet<DocumentDetail> DocumentDetails { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
