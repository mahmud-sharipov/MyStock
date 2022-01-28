

namespace MyStock.ViewModels;

public class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        Menu = MenuHelper.GetMenu().OrderBy(m => m.Order).ToList();
    }
    
    #region Properties

    public List<MenuItemModel> Menu { get; set; }

    #endregion

    #region Commands

    public RelayCommand Navigate => new RelayCommand(
        param =>
        {
            if (param is MenuItemModel menu)
                NavigationMananger.NavigationService.Navigate(menu.Url);
        },
        param =>
        {
            if (param is MenuItemModel menu)
                return NavigationMananger.NavigationService.CurrentSource.ToString() != menu.Url?.ToString();

            return true;
        }
    );

    #endregion
}