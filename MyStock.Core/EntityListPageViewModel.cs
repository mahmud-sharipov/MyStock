namespace MyStock.Core;

public abstract class EntityListPageViewModel<TEntity, TPage> : EntityListViewModel<TEntity>, IEntityListPageViewModel<TEntity>
    where TEntity : class, IEntity, new()
    where TPage : class, IEntityListPage
{
    TPage _entityPage;
    int _collectionSize;
    string _title;

    protected EntityListPageViewModel(IContext context) : base(context)
    {
        CanOpen = this.WhenAny(e => e.SelectedItem, si => si.Value != null);
        CanOpen = this.WhenAny(e => e.Context, si => true);
        BuildCommands();
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

    public ICommand Delete { get; set; }

    public IObservable<bool> CanAdd { get; protected set; }

    public IObservable<bool> CanOpen { get; protected set; }

    public int CollectionSize
    {
        get => _collectionSize;
        set => this.RaiseAndSetIfChanged(ref _collectionSize, value);
    }

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public TPage EntityPage => _entityPage ??= Global.Container.Resolve<TPage>(new PositionalParameter(0, this));

    IEntityListPage INavigatable.EntityPage => EntityPage;

    object IEntityListPageViewModel.SelectedItem
    {
        get => SelectedItem;
        set => SelectedItem = value as TEntity;
    }

    protected virtual void BuildCommands()
    {
        Add = ReactiveCommand.Create(async () =>
        {
            var entity = new TEntity();
            var viewModel = CreateEntityViewModel(entity);
            Context.AddToContext(entity);
            var result = await viewModel.DialogHost.Show(viewModel.EntityPage, IDialogHost.RootDialogIdentifier);
            if (true.Equals(result))
                _collection.Add(entity);
        }, CanAdd, outputScheduler: Scheduler.CurrentThread);

        Open = ReactiveCommand.Create<TEntity>((param) =>
        {
            var entity = param ?? SelectedItem;
            if (entity != null)
            {
                var viewModel = CreateEntityViewModel(entity);
                viewModel.DialogHost.Show(viewModel.EntityPage, IDialogHost.RootDialogIdentifier);
            }
        }, CanOpen, outputScheduler: Scheduler.CurrentThread);

        Delete = ReactiveCommand.Create<TEntity>((param) =>
        {
            if (param is not TEntity entity) return;

            if (CanDeleteEntity(entity, out string reason))
            {
                Context.Delete(entity);
                Context.SaveChanges();
                _collection.Remove(entity);
            }
            else
            {
                var message = Global.Container.Resolve<IMessage>();
                message.Detail = reason;
                message.Severity = SeverityLevel.Error;
                message.Show();
            }
        }, outputScheduler: Scheduler.CurrentThread);
    }

    protected abstract ICollection<ColumnViewModel> BuildColums();

    protected abstract IViewable CreateEntityViewModel(TEntity entity);
}
