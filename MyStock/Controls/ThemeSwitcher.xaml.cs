namespace MyStock.Controls;

public partial class ThemeSwitcher : UserControl
{
    public ThemeSwitcher()
    {
        InitializeComponent();
        if (AppManager.UISettings.Theme == UITheme.Dark)
        {
            ChangeToDarkBtn.Visibility = Visibility.Hidden;
            ChangeToLightBtn.Visibility = Visibility.Visible;
        }
        else
        {
            ChangeToDarkBtn.Visibility = Visibility.Visible;
            ChangeToLightBtn.Visibility = Visibility.Hidden;
        }

        DataContext = this;
    }

    private void ChangeToDarkTheme(object sender, RoutedEventArgs e)
    {
        AppManager.ChangeTheme(UITheme.Dark);
        ChangeToDarkBtn.Visibility = Visibility.Hidden;
        ChangeToLightBtn.Visibility = Visibility.Visible;
    }

    private void ChangeToLightTheme(object sender, RoutedEventArgs e)
    {
        AppManager.ChangeTheme(UITheme.Light);
        ChangeToDarkBtn.Visibility = Visibility.Visible;
        ChangeToLightBtn.Visibility = Visibility.Hidden;
    }
}
