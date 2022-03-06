namespace MyStock.Application.Category;

public class ProductCategoryViewModel : EntityPageViewModel<ProductCategory, ProductCategoryValidator, IProductCategoryEntityPage>
{
    private string _name;
    private ProductCategory _parent;

    public ProductCategoryViewModel(ProductCategory entity, IContext context) : base(entity, context)
    {
    }

    public string Name { get => _name; set => RaiseAndSetAndValidateIfChanged(ref _name, value); }
    public ProductCategory Parent { get => _parent; set => RaiseAndSetAndValidateIfChanged(ref _parent, value); }

    public ProductCategorySearchViewModel CategorySearchViewModel => new ProductCategorySearchViewModel(Context, c => Parent = c)
    {
        SelectedSearchItem = Entity.Parent,
        SearchText = Entity.Parent?.Title ?? ""
    };

    protected override void InitializeAssociatedProperties()
    {
        base.InitializeAssociatedProperties();
    }

    public override bool ValidateBeforeSaveChange()
    {
        return base.ValidateBeforeSaveChange();
    }

    public override void ActionBeforeSave()
    {
        base.ActionBeforeSave();
    }
}
