namespace MyStock.Application.Customers;

public class CustomerSearchViewModel : SearchView.SearchViewModel<Customer>
{
    private List<Customer> customers;

    public CustomerSearchViewModel(IContext context, Action<Customer> onSelected) : base(context, onSelected)
    {
    }

    private List<Customer> Customers => customers ??= Items.ToList();

    protected override IQueryable<Customer> GetAllItems() =>
        Customers.AsQueryable();

    protected override IQueryable<Customer> Filter(IQueryable<Customer> source) =>
        source.Where(c => c.Title.ToLower().StartsWith(SearchText.ToLower()));

    protected override IOrderedQueryable<Customer> Order(IQueryable<Customer> source) =>
        source.OrderBy(c => c.FirstName);
}
