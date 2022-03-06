namespace MyStock.Core;

public abstract class ViewModel : ReactiveObject, IViewModel
{
    private bool isDisposed;

    protected ViewModel(IContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Token = Guid.NewGuid();
    }

    public Guid Token { get; }
    public IContext Context { get; private set; }
    public bool IsNew { get; set; }

    public void Dispose()
    {
        OnDisposing();
        GC.SuppressFinalize(this);
    }

    protected virtual void OnDisposing()
    {
        if (isDisposed) return;

        Context.Dispose();
        Context = null;
        isDisposed = true;
    }

    protected bool RaiseAndSetIfChanged<T>(ref T backingField, T newValue, [CallerMemberName] string propertyName = null)
    {
        _ = propertyName ?? throw new ArgumentNullException(nameof(propertyName));

        if (EqualityComparer<T>.Default.Equals(backingField, newValue))
            return false;

        this.RaisePropertyChanging(propertyName);
        backingField = newValue;
        this.RaisePropertyChanged(propertyName);
        return true;
    }

    protected bool RaiseAndSetIfChanged<T>(ref T backingField, T newValue, string propertyName = null, params string[] dependentProperties)
    {
        var result = RaiseAndSetIfChanged(ref backingField, newValue, propertyName);
        if (result)
            RaisePropertyChanged(dependentProperties);
        return result;
    }

    protected void RaisePropertyChanged(string[] dependentProperties)
    {
        for (int i = 0; i < dependentProperties.Length; i++)
            this.RaisePropertyChanged(dependentProperties[i]);
    }
}
