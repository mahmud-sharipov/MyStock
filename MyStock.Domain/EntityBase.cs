using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyStock.Domain;

public class EntityBase : IEntity, INotifyPropertyChanged
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

    public string Title => ToString();

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetProptery<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
    {
        if (!EqualityComparer<T>.Default.Equals(field, newValue))
        {
            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }
        return false;
    }

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion
}
