namespace MyStock.Core.Interfaces;

public interface IEntityListPage
{
    INavigatable ViewModel { get; set; }
    bool IsDisposed { get; set; }

}
