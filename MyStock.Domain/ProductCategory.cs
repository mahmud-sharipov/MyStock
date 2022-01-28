using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyStock.Domain;

public class ProductCategory : EntityBase
{
    private string name;
    private ProductCategory parent;

    public ProductCategory()
    {
        Products = new HashSet<Product>();
        ChildCategories = new ObservableCollection<ProductCategory>();
    }

    public string Name
    {
        get => name;
        set => SetProptery(ref name, value);
    }

    public Guid? ParentGuid { get; set; }
    public virtual ProductCategory Parent
    {
        get => parent;
        set => SetProptery(ref parent, value);
    }

    public virtual ObservableCollection<ProductCategory> ChildCategories { get; set; }
    public virtual ISet<Product> Products { get; set; }
}