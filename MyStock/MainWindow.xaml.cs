namespace MyStock;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        
        DataContext = new MainViewModel();
    }

    private void ButtonFechar_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}