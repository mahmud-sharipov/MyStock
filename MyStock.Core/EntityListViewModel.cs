using DynamicData;
using System.Collections.ObjectModel;

namespace MyStock.Core;

public abstract class EntityListViewModel<TEntity> : ViewModel, IEntityListViewModel<TEntity>
    where TEntity : class, IEntity
{
    public EntityListViewModel(IContext context) : base(context)
    {
        Source = context.Set<TEntity>();
    }

    protected ObservableCollection<TEntity> _collection = new ObservableCollection<TEntity>();
    public ObservableCollection<TEntity> Collection
    {
        get
        {
            _collection.Clear();
            _collection.AddRange(Source.Where(FilereItem));
            return _collection;
        }
    }

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