namespace MyStock.Domain;

public class ProductCategory : EntityBase
{
    public ProductCategory()
    {
        Products = new HashSet<Product>();
        ChildCategories = new HashSet<ProductCategory>();
    }

    public string Name { get; set; }

    public Guid ParentGuid { get; set; }
    public virtual ProductCategory Parent { get; set; }

    public virtual ISet<ProductCategory> ChildCategories { get; set; }
    public virtual ISet<Product> Products { get; set; }
}
