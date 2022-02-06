namespace MyStock.Core.Interfaces;

public interface IEntityListViewModel<T> : IViewModel where T : class, IEntity
{
    bool CanDeleteEntity(T entity, out string reason);
    ICollection<T> Collection { get; }
    T SelectedItem { get; set; }
}