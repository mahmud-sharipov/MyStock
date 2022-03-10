using MyStock.Application.Assets.Lang;
using MyStock.Application.Settings;
using MyStock.Core.Interfaces;
using System.Linq;

namespace MyStock.Pages.Settings;

public partial class SettingsPage : ISettingsPage
{
    INavigatable IEntityListPage.ViewModel { get => ViewModel; set => ViewModel = value as SettingsViewModel; }

    public SettingsPage(SettingsViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
        Loaded += OnPageLoad;
    }

    private void OnPageUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnPageUnloaded;
        Loaded -= OnPageLoad;
        ViewModel?.Dispose();
        ViewModel = null;
        IsDisposed = true;
        Global.Context.SaveChanges();
    }

    private void OnPageLoad(object sender, RoutedEventArgs e)
    {
        Unloaded += OnPageUnloaded;
    }

    private void UpdateUserInfo_Click(object sender, RoutedEventArgs e)
    {
        Error.Text = "";
        if (string.IsNullOrWhiteSpace(FirstName.Text))
            Error.Text += $"{Translations.FirstName} обязательно к заполнению!\r\n";

        if (string.IsNullOrWhiteSpace(LastName.Text))
            Error.Text += $"{Translations.LastName} обязательно к заполнению!\r\n";

        if (string.IsNullOrWhiteSpace(Login.Text))
            Error.Text += $"{Translations.EnterUsername} обязательно к заполнению!\r\n";

        if (string.IsNullOrWhiteSpace(Password.Password))
            Error.Text += $"{Translations.EnterPassword} обязательно к заполнению!\r\n";

        if (!string.IsNullOrWhiteSpace(Error.Text))
            return;

        if (Password.Password.Trim() != ConfirmPassword.Password.Trim())
            Error.Text += $"{Translations.EnterPassword} и подтверждение не совпадают!\r\n";

        if (!string.IsNullOrWhiteSpace(Error.Text))
            return;

        if (Global.Context.Set<User>().Any(u => u.Guid != Global.CurrentUser.Guid && u.Login.ToLower() == Login.Text.Trim().ToLower()))
            Error.Text = $"{Translations.EnterUsername} обязательно к заполнению!\r\n";

        if (!string.IsNullOrWhiteSpace(Error.Text))
            return;

        var pass = UserManager.EncryptPassword(Password.Password.Trim());
        Global.CurrentUser.LastName = LastName.Text;
        Global.CurrentUser.FirstName = FirstName.Text;
        Global.CurrentUser.MiddleName = MiddleName.Text;
        Global.CurrentUser.Login = Login.Text;
        Global.CurrentUser.PasswordHash = pass.Hash;
        Global.CurrentUser.Salt = pass.Salt;
        Global.Context.SaveChanges();

        Password.Password = "";
        ConfirmPassword.Password = "";
    }

}
