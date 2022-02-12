namespace MyStock.Application.Customers;

public class CustomerSearchViewModel : SearchView.SearchViewModel<Customer>
{
    public CustomerSearchViewModel(IContext context, Action<Customer> onSelected) : base(context, onSelected)
    {
        Customers = Items.ToList();
        SearchResults = Customers.OrderBy(c => c.FirstName).Take(ResultCount).ToObservableCollection();
    }

    public List<Customer> Customers { get; set; }

    protected override void UpdateSearchResults()
    {
        IEnumerable<Customer> result = Customers;
        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            var names = SearchText.Split(" ").Where(n => !string.IsNullOrEmpty(n)).ToArray();
            result = Customers.Where(c =>
                c.Title.ToLower().StartsWith(SearchText.ToLower()));
        }
        var selectdItem = SelectedSearchItem;
        SearchResults = result.OrderBy(c => c.FirstName).Take(ResultCount).ToObservableCollection();
        if (selectdItem != null)
            SelectedSearchItem = SearchResults.SingleOrDefault(e => e.Guid == selectdItem.Guid);
    }
}
