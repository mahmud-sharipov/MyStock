namespace MyStock;

public partial class MainWindow
{
    public MainWindow()
    {
        InitializeComponent();
        ContentGrid.Children.Add(NavigationMananger.Instance.MainFrame);
        DataContext = new MainViewModel();
        MainSnackbar.MessageQueue = NotificationManager.Instance.SnackbarMessageQueue;
    }

    private void ButtonFechar_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.Shutdown();
    }
}