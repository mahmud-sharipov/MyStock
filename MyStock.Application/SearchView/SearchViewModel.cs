namespace MyStock.Application.SearchView;

public abstract class SearchViewModel<TEntity> : ReactiveObject where TEntity : class, IEntity
{
    public const int ResultCount = 10;
    public SearchViewModel(IContext context, Action<TEntity> onSelected)
    {
        Context = context;
        OnSelected = onSelected;
        this.PropertyChanged += SearchViewModel_PropertyChanged;
        Items = context.Set<TEntity>();
        SearchResults = Order(GetAllItems()).Take(ResultCount).ToObservableCollection();
    }

    protected virtual IQueryable<TEntity> Items { get; }

    ObservableCollection<TEntity> _searchResults;
    public ObservableCollection<TEntity> SearchResults
    {
        get => _searchResults;
        set => this.RaiseAndSetIfChanged(ref _searchResults, value);
    }

    bool _isSearchShown;
    public bool IsSearchResultShown
    {
        get => _isSearchShown;
        set => this.RaiseAndSetIfChanged(ref _isSearchShown, value);
    }

    string _searchText_;
    public string SearchText
    {
        get => _searchText_;
        set => this.RaiseAndSetIfChanged(ref _searchText_, value);
    }

    TEntity _selectedSearchItem;
    public TEntity SelectedSearchItem
    {
        get => _selectedSearchItem;
        set => this.RaiseAndSetIfChanged(ref _selectedSearchItem, value);
    }

    public IContext Context { get; }
    public Action<TEntity> OnSelected { get; }

    private void SearchViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SearchText))
            OnSearchTextChanged();
        else if (e.PropertyName == nameof(SelectedSearchItem))
            OnSelectedSearchItemChanged();
    }

    private void OnSelectedSearchItemChanged()
    {
        OnSelected?.Invoke(SelectedSearchItem);
        SearchText = SelectedSearchItem?.Title?.ToString() ?? "";
        IsSearchResultShown = false;
    }

    private void OnSearchTextChanged()
    {
        UpdateSearchResults();
        IsSearchResultShown = true;
    }

    void UpdateSearchResults()
    {
        IQueryable<TEntity> result = GetAllItems();
        if (!string.IsNullOrWhiteSpace(SearchText))
            result = Filter(result);

        var newResult = Order(result).Take(ResultCount).ToList();
        var notExistItems = SearchResults.Where(r => !newResult.Contains(r)).ToList();
        foreach (var item in notExistItems)
            SearchResults.Remove(item);
        foreach (var item in newResult)
        {
            if (SearchResults.Contains(item))
                continue;
            SearchResults.Add(item);
        }
    }

    protected virtual IQueryable<TEntity> GetAllItems() => Items;

    protected virtual IQueryable<TEntity> Filter(IQueryable<TEntity> source) => source;

    protected virtual IOrderedQueryable<TEntity> Order(IQueryable<TEntity> source) =>
        source.OrderBy(e => e.Guid);
}
