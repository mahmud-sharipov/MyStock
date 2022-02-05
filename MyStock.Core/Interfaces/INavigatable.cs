namespace MyStock.Core.Interfaces;

public interface INavigatable : IViewModel
{
    IEntityListPage EntityPage { get; }
}
