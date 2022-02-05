namespace MyStock.Core.Interfaces;

public interface IViewModel : IDisposable, IReactiveObject
{
    Guid Token { get; }
    IContext Context { get; }
}