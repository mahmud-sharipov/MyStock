namespace MyStock.Pages;

public class BasePage<TViewModel> : Page
{
    public TViewModel ViewModel { get; set; }

    public BasePage(TViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;
    }
}