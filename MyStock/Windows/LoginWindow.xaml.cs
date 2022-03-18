using MyStock.Application.Assets.Lang;
using System.Windows.Input;

namespace MyStock.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = this;
            //txtUsername.Text = "Admin";
            //txtPassword.Password = "Admin";
            //loginBtn_Click(null, null);
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            if (UserManager.Login(txtUsername.Text, txtPassword.Password))
            {
                new MainWindow().Show();
                this.Close();
            }
            ErrorBlock.Text = Translations.LoginOrPasswordWrong;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
