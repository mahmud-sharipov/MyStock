namespace MyStock.Core;

public abstract class EntityListPageViewModel<TEntity> : EntityListViewModel<TEntity>, IEntityListPageViewModel<TEntity>
    where TEntity : class, IEntity, new()
{
    protected EntityListPageViewModel(IContext context) : base(context)
    {
        CanOpen = this.WhenAny(e => e.SelectedItem, si => si.Value != null);
        CanOpen = this.WhenAny(e => e.Context, si => true);
    }

    ICollection<ColumnViewModel> _colums;
    public ICollection<ColumnViewModel> Columns
    {
        get
        {
            if (_colums == null)
                _colums = BuildColums();
            return _colums;
        }
    }

    public ICommand Add { get; set; }

    public ICommand Open { get; set; }

    public IObservable<bool> CanAdd { get; protected set; }

    public IObservable<bool> CanOpen { get; protected set; }

    int _collectionSize;
    public int CollectionSize
    {
        get => _collectionSize;
        set => this.RaiseAndSetIfChanged(ref _collectionSize, value);
    }

    string _title;
    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    protected virtual void BuildCommands()
    {
        Add = ReactiveCommand.Create(() =>
        {
            var entity = new TEntity();
            var viewModel = CreateEntityViewModel(entity);
            Context.AddToContext(entity);
            viewModel.DialogHost.Show(viewModel.EntityPage, IDialogHost.RootDialogIdentifier);
        }, CanAdd, outputScheduler: Scheduler.CurrentThread);

        Add = ReactiveCommand.Create(() =>
        {
            var viewModel = CreateEntityViewModel(SelectedItem);
            viewModel.DialogHost.Show(viewModel.EntityPage, IDialogHost.RootDialogIdentifier);
        }, CanOpen, outputScheduler: Scheduler.CurrentThread);
    }

    protected abstract ICollection<ColumnViewModel> BuildColums();

    protected abstract IViewable CreateEntityViewModel(TEntity entity);
}
