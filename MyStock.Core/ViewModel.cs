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
}
