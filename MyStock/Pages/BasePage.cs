using MyStock.Core.Interfaces;

namespace MyStock.Pages;

public class BasePage<TViewModel> : ReactiveUserControl<TViewModel>
    where TViewModel : class, IViewModel
{
    public bool IsDisposed { get; set; }

    public BasePage(TViewModel viewModel)
    {
        ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        DataContext = viewModel;
    }
}