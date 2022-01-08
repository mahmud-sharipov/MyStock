namespace MyStock.ViewModels;

public class MainViewModel : BaseViewModel
{
    public MainViewModel()
    {
        //Router = new RoutingState();
        Menu = new List<MenuItemModel>();
        CreateMenu();
    }

    #region Properties

    public List<MenuItemModel> Menu { get; set; }
    //public RoutingState Router { get; }

    #endregion

    #region Commands

    public RelayCommand? Navigate { get; }
    public RelayCommand? GoNext { get; }
    public RelayCommand? GoBack { get; }

    #endregion

    void CreateMenu()
    {
        Menu.Add(Helper.Menu.DashboardPage);
        Menu.Add(Helper.Menu.Products);
        Menu.Add(Helper.Menu.Orders);
        Menu.Add(Helper.Menu.Sales);
        Menu.Add(Helper.Menu.Purchases);
        Menu.Add(Helper.Menu.Customers);
        Menu.Add(Helper.Menu.Vendors);
    }
}