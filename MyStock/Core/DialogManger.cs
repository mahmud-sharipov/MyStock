using System.Threading.Tasks;

namespace MyStock.Core;

public class DialogManger
{
    private static readonly object padlock = new object();
    private static DialogManger instance;
    private static readonly string _rootDialogIdentifier = "RootDialog";
    DialogManger() { }

    public static DialogManger Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new DialogManger();
                return instance;
            }
        }
    }

    public static async Task<bool> Show<TDialogView, TViewModel>(TViewModel viewModel)
        where TDialogView : UserControl, new()
        where TViewModel : class
    {
        var view = new TDialogView()
        {
            DataContext = viewModel,
        };
        bool succeeded = false;
        var result = await DialogHost.Show(view, _rootDialogIdentifier);
        bool.TryParse(result?.ToString(), out succeeded);
        return succeeded;
    }
}
