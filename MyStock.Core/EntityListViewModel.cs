using DynamicData;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

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
            _collection.AddRange(Order(Source.Where(FilereItem())));
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

    protected virtual Expression<Func<TEntity, bool>> FilereItem()
    {
        return entity => true;
    }

    protected virtual IOrderedQueryable<TEntity> Order(IQueryable<TEntity> source)
    {
        return source.OrderBy(e => e.Guid);
    }
}