namespace MyStock.Application.Products;

public class ProductSearchViewModel : SearchView.SearchViewModel<Product>
{
    public ProductSearchViewModel(IContext context, Action<Product> onSelected) : base(context, onSelected)
    {
        SearchResults = Items.OrderBy(c => c.Description).Take(ResultCount).ToObservableCollection();
    }

    protected override IQueryable<Product> Filter(IQueryable<Product> source)
    {
        var searchText = SearchText.ToLower();
        return source.Where(c => c.Description.ToLower().Contains(searchText) || c.Code.ToLower().Contains(searchText));
    }

    protected override IOrderedQueryable<Product> Order(IQueryable<Product> source) =>
        source.OrderBy(c => c.Description);
}
