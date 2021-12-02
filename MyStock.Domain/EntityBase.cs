namespace MyStock.Domain;

public class EntityBase : IEntity
{
    private Guid _id;

    public Guid Guid
    {
        get
        {
            if (_id == Guid.Empty)
                _id = Guid.NewGuid();

            return _id;
        }
        set => _id = value;
    }
}
