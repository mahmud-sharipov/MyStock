namespace MyStock.Helper;
public static class Menu
{
    public static MenuItemModel DashboardPage = new()
    {
        Name = "Dashboard",
        Icon = "Home",
        Description = "",
        Url = new Uri("/Pages/DashboardPage.xaml", UriKind.Relative)
    };

    public static MenuItemModel Products = new()
    {
        Name = "Products",
        Icon = "PackageVariant",
        Description = "",
        Url = new Uri("/Pages/ProductsPage.xaml", UriKind.Relative)
    };

    public static MenuItemModel Orders = new()
    {
        Name = "Orders",
        Icon = "FormatListChecks",
        Description = "",
        Url = new Uri("/Pages/DashboardPage.xaml", UriKind.Relative)
    };

    public static MenuItemModel Sales = new()
    {
        Name = "Sales",
        Icon = "ClipboardList",
        Description = "",
        Url = new Uri("/Pages/DashboardPage.xaml", UriKind.Relative)
    };

    public static MenuItemModel Purchases = new()
    {
        Name = "Purchases",
        Icon = "Cart",
        Description = "",
        Url = new Uri("/Pages/DashboardPage.xaml", UriKind.Relative)
    };

    public static MenuItemModel Customers = new()
    {
        Name = "Customers",
        Icon = "AccountGroup",
        Description = "",
        Url = new Uri("/Pages/DashboardPage.xaml", UriKind.Relative)
    };

    public static MenuItemModel Vendors = new()
    {
        Name = "Vendors",
        Icon = "AccountGroup",
        Description = "",
        Url = new Uri("/Pages/DashboardPage.xaml", UriKind.Relative)
    };

}