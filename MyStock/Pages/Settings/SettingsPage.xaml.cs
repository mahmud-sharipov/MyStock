using MyStock.Application.Settings;
using MyStock.Core.Interfaces;

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
    }

    private void OnPageLoad(object sender, RoutedEventArgs e)
    {
        Unloaded += OnPageUnloaded;
    }
}
