namespace MyStock.Application.Category;

public class ProductCategorySearchViewModel : SearchView.SearchViewModel<ProductCategory>
{
    public ProductCategorySearchViewModel(IContext context, Action<ProductCategory> onSelected) : base(context, onSelected)
    {
    }

    protected override IQueryable<ProductCategory> Filter(IQueryable<ProductCategory> source) =>
        source.Where(c => c.Name.ToLower().StartsWith(SearchText.ToLower()));

    protected override IOrderedQueryable<ProductCategory> Order(IQueryable<ProductCategory> source) =>
        source.OrderBy(c => c.Name);
}
