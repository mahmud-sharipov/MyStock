namespace MyStock.Core;

public abstract class EntityListViewModel<TEntity> : ViewModel, IEntityListViewModel<TEntity>
    where TEntity : class, IEntity
{
    public EntityListViewModel(IContext context) : base(context)
    {
        Source = context.Set<TEntity>();
    }

    protected IQueryable<TEntity> Source { get; }
    public IEnumerable<TEntity> Collection { get; protected set; }

    TEntity _selectedItem;
    public TEntity SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }

    public abstract void Delete(TEntity entity);
}