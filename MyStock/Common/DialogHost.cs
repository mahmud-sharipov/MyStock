using MyStock.Core.Interfaces;
using System.Threading.Tasks;

namespace MyStock.Common;

public class DialogHost : IDialogHost
{
    public async Task<object> Show(object content, object dialogIdentifier) =>
      await MaterialDesignThemes.Wpf.DialogHost.Show(content, dialogIdentifier);

    public void Close(IEntityPage view, bool succeeded = false) =>
        MaterialDesignThemes.Wpf.DialogHost.CloseDialogCommand.Execute(succeeded, view as IInputElement);
}
