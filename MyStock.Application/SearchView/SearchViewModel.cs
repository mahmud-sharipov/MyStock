namespace MyStock.Application.SearchView;

public abstract class SearchViewModel<T> : ReactiveObject where T : class, IEntity
{
    public const int ResultCount = 10;
    public SearchViewModel(IContext context, Action<T> onSelected)
    {
        Context = context;
        OnSelected = onSelected;
        this.PropertyChanged += SearchViewModel_PropertyChanged;
        Items = context.Set<T>();
    }

    private void SearchViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SearchText))
            OnSearchTextChanged();
        else if (e.PropertyName == nameof(SelectedSearchItem))
            OnSelectedSearchItemChanged();
    }

    protected virtual IQueryable<T> Items { get; }

    ObservableCollection<T> _searchResults;
    public ObservableCollection<T> SearchResults
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

    string _searchText;
    public string SearchText
    {
        get => _searchText;
        set => this.RaiseAndSetIfChanged(ref _searchText, value);
    }

    T _selectedSearchItem;
    public T SelectedSearchItem
    {
        get => _selectedSearchItem;
        set => this.RaiseAndSetIfChanged(ref _selectedSearchItem, value);
    }

    public IContext Context { get; }
    public Action<T> OnSelected { get; }

    private void OnSelectedSearchItemChanged()
    {
        if (SelectedSearchItem == null)
            return;

        OnSelected?.Invoke(SelectedSearchItem);
        SearchText = SelectedSearchItem?.Title?.ToString();
        IsSearchResultShown = false;
    }

    private void OnSearchTextChanged()
    {
        UpdateSearchResults();
        IsSearchResultShown = true;
    }

    protected abstract void UpdateSearchResults();
}
