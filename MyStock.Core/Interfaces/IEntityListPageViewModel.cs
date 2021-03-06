namespace MyStock.Core.Interfaces
{
    public interface IEntityListPageViewModel : INavigatable
    {
        ICommand Add { get; }
        ICommand Open { get; }
        ICommand Delete { get; }
        IObservable<bool> CanAdd { get; }
        IObservable<bool> CanOpen { get; }
        int CollectionSize { get; set; }
        string Title { get; }
        object SelectedItem { get; set; }
        ICollection<ColumnViewModel> Columns { get; }
    }

    public interface IEntityListPageViewModel<TEntity> : IEntityListPageViewModel, IEntityListViewModel<TEntity> where TEntity : class, IEntity
    {

    }
}
