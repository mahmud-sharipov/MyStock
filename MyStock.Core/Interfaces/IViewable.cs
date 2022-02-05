namespace MyStock.Core.Interfaces;
public interface IViewable: IViewModel
{
    IDialogHost DialogHost { get; }
    IEntityPage EntityPage { get; }
    ICommand Close { get; }
    ICommand SaveChange { get; }
}
