namespace MyStock.Helper;
public static class MenuHelper
{
    public static MenuItemModel Dashboard = new("Dashboard", "Home", "Pages/DashboardPage.xaml", 1, "");
    public static MenuItemModel Products = new("Products", "PackageVariant", "Pages/ProductsPage.xaml", 2, "");
    public static MenuItemModel UOMs = new("UOMs", "Ruler", "Pages/UOMs.xaml", 3, "");
    public static MenuItemModel Orders = new("Orders", "FormatListChecks", "Pages/DashboardPage.xaml", 4, "");
    public static MenuItemModel Sales = new("Sales", "ClipboardList", "Pages/DashboardPage.xaml", 5, "");
    public static MenuItemModel Purchases = new("Purchases", "Cart", "Pages/DashboardPage.xaml", 6, "");
    public static MenuItemModel Customers = new("Customers", "AccountGroup", "Pages/CustomersPage.xaml", 7, "");
    public static MenuItemModel Vendors = new("Vendors", "AccountGroup", "Pages/DashboardPage.xaml", 8, "");

    public static IEnumerable<MenuItemModel> GetMenu() => new[] {
        Dashboard,
        Products,
        UOMs,
        Orders,
        Sales,
        Purchases,
        Customers,
        Vendors
    };
}