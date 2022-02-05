using MyStock.Core.Interfaces;
using System.Threading.Tasks;

namespace MyStock.Common;

public class DialogHost : IDialogHost
{
    public Task<object> Show(object content, object dialogIdentifier) =>
        MaterialDesignThemes.Wpf.DialogHost.Show(content, dialogIdentifier);

    public void Close(IEntityPage view) =>
        MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(false, view as IInputElement);
}
