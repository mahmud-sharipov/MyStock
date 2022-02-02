namespace MyStock.Core.Interfaces
{

    public interface IEntityListPageViewModel<TEntity> : IEntityListViewModel<TEntity> where TEntity : class, IEntity
    {
        ICollection<ColumnViewModel> Columns { get; }
        ICommand Add { get; }
        ICommand Open { get; }
        IObservable<bool> CanAdd { get; }
        IObservable<bool> CanOpen { get; }
        int CollectionSize { get; set; }
        string Title { get; }
    }
}
