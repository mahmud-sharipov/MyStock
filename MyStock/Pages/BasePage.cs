using MyStock.Core.Interfaces;

namespace MyStock.Pages;

public class BasePage<TViewModel> : ReactiveUI.ReactiveUserControl<TViewModel>
    where TViewModel : class, IViewModel
{
    public bool IsDisposed { get; set; }

    public BasePage(TViewModel viewModel)
    {
        ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));
        DataContext = viewModel;
        Loaded += OnPageLoad;
    }

    private void OnPageUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnPageUnloaded;
        Loaded -= OnPageLoad;
        ViewModel?.Dispose();
        ViewModel = null;
        IsDisposed = true;
    }

    private void OnPageLoad(object sender, RoutedEventArgs e)
    {
        Unloaded += OnPageUnloaded;
    }
}