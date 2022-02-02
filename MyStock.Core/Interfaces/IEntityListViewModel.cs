namespace MyStock.Core.Interfaces;

public interface IEntityListViewModel<T> : IViewModel where T : class, IEntity
{
    void Delete(T entity);
    IEnumerable<T> Collection { get; }
    T SelectedItem { get; set; }
}