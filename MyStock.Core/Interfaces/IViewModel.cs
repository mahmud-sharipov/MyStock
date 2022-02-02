namespace MyStock.Core.Interfaces;

public interface IViewModel : IReactiveObject
{
    Guid Token { get; }
    IContext Context { get; }
}