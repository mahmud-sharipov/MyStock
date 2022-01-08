using System.Windows.Input;

namespace MyStock.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            IsDarkTheme = AppMananger.UISettings.Theme == UITheme.Dark;
            InitializeComponent();
            DataContext = this;
        }

        public bool IsDarkTheme { get; set; }

        private void toggleTheme(object sender, RoutedEventArgs e)
        {
            AppMananger.ChangeTheme(IsDarkTheme ? UITheme.Dark : UITheme.Light);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
