namespace MyStock.Core.Interfaces;

public interface IContext : IDisposable
{
    void AddToContext<T>(T entity) where T : IEntity;
    void MarkDelete<T>(T entity) where T : IEntity;
    IQueryable<T> Set<T>() where T : class, IEntity;
    T Get<T>(Guid guid) where T : class, IEntity;
    void Update<T>(Guid id, T entity) where T : class, IEntity;
    void Delete<T>(T entity) where T : class, IEntity;
    bool HasChange();
    int SaveChanges();
    void UndoChanges();
}