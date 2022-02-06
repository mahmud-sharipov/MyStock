namespace MyStock.Core.Interfaces;

public interface IDialogHost
{
    Task<object> Show(object content, object dialogIdentifier);

    void Close(IEntityPage view, bool succeeded = false);

    public static string RootDialogIdentifier = "RootDialog";
}
