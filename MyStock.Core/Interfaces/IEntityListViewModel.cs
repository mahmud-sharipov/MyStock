using System.Collections.ObjectModel;

namespace MyStock.Core.Interfaces;

public interface IEntityListViewModel<T> : IViewModel where T : class, IEntity
{
    bool CanDeleteEntity(T entity, out string reason);
    ObservableCollection<T> Collection { get; }
    T SelectedItem { get; set; }
}