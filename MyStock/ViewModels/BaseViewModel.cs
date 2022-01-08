using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MyStock.ViewModels;

public abstract class BaseViewModel : IDisposable, INotifyPropertyChanged
{
    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetProptery<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
    {
        if (!EqualityComparer<T>.Default.Equals(field, newValue))
        {
            field = newValue;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            return true;
        }
        return false;
    }

    #endregion

    #region IDisposable

    public void Dispose() =>
            OnDispose();

    protected virtual void OnDispose() { }

    #endregion
}
