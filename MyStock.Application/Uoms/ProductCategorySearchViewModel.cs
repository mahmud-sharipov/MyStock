namespace MyStock.Application.Uoms;

public class UomSearchViewModel : SearchView.SearchViewModel<Uom>
{
    public UomSearchViewModel(IContext context, Action<Uom> onSelected) : base(context, onSelected)
    {
    }

    protected override IQueryable<Uom> Filter(IQueryable<Uom> source) =>
        source.Where(c => c.Name.ToLower().StartsWith(SearchText.ToLower()) || c.Code.ToLower().StartsWith(SearchText.ToLower()));

    protected override IOrderedQueryable<Uom> Order(IQueryable<Uom> source) =>
        source.OrderBy(c => c.Name);
}
