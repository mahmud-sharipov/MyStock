namespace MyStock.Core.Interfaces;

public interface IDialogHost
{
    Task<object> Show(object content, object dialogIdentifier);

    void Close(object view);

    public static string RootDialogIdentifier = "RootDialog";
}
