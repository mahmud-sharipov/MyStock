using DynamicData;
using System.Collections.ObjectModel;
using System.Reactive;

namespace MyStock.Core;

public abstract class EntityListViewModel<TEntity> : ViewModel, IEntityListViewModel<TEntity>
    where TEntity : class, IEntity
{
    public EntityListViewModel(IContext context) : base(context)
    {
        Source = context.Set<TEntity>();
    }
    public ICollection<TEntity> Collection => new ObservableCollection<TEntity>(Source.Where(FilereItem));

    protected IQueryable<TEntity> Source { get; }

    TEntity _selectedItem;
    public TEntity SelectedItem
    {
        get => _selectedItem;
        set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
    }

    public abstract bool CanDeleteEntity(TEntity entity, out string reason);

    protected virtual bool FilereItem(TEntity entity)
    {
        return true;
    }
}