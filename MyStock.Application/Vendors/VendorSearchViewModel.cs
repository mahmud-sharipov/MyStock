namespace MyStock.Application.Vendors;

public class VendorSearchViewModel : SearchView.SearchViewModel<Vendor>
{
    private List<Vendor> _vendors;

    public VendorSearchViewModel(IContext context, Action<Vendor> onSelected) : base(context, onSelected)
    {
    }

    private List<Vendor> Vendors => _vendors ??= Items.ToList();

    protected override IQueryable<Vendor> GetAllItems() =>
        Vendors.AsQueryable();

    protected override IQueryable<Vendor> Filter(IQueryable<Vendor> source) =>
        source.Where(c => c.Title.ToLower().StartsWith(SearchText.ToLower()));

    protected override IOrderedQueryable<Vendor> Order(IQueryable<Vendor> source) =>
        source.OrderBy(c => c.FirstName);
}
