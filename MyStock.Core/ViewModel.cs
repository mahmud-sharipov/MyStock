using MyStock.Core.Interfaces;

namespace MyStock.Core;

public abstract class ViewModel : ReactiveObject, IViewModel
{
    protected ViewModel(IContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
        Token = Guid.NewGuid();
    }

    public Guid Token { get; }
    public IContext Context { get; }
}
