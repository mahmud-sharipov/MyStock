namespace MyStock.Pages;

public class BasePage<TViewModel> : Page where TViewModel : BaseViewModel
{
    public TViewModel ViewModel { get; set; }

    public BasePage(TViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
        Loaded += OnPageLoad;
    }

    private void OnPageUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnPageUnloaded;
        Loaded -= OnPageLoad;
        ViewModel?.Dispose();
        ViewModel = null;
    }

    private void OnPageLoad(object sender, RoutedEventArgs e)
    {
        Unloaded += OnPageUnloaded;
    }
}