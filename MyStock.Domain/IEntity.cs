namespace MyStock.Domain;

public interface IEntity
{
    public Guid Guid { get; }
    public string Title { get; }
}
