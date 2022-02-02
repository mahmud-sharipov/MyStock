namespace MyStock.Core.Interfaces;

public interface IEntityViewModel<T> : IViewModel where T : class, IEntity
{
    T Entity { get; }
    ICommand SaveChange { get; }
    bool ValidateBeforeSaveChange();
    void ActionBeforeSave();
}
