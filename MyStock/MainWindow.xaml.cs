using MyStock.ViewModels;
using ReactiveUI;

namespace MyStock;

public partial class MainWindow : IViewFor<MainViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        DataContext = ViewModel;
    }

    public MainViewModel ViewModel { get; set; }
    object IViewFor.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as MainViewModel;
    }
}