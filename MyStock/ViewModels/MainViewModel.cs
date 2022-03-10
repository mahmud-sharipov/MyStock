using Autofac;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MyStock.Application.Assets.Lang;
using MyStock.Application.Customers;
using MyStock.Application.Dashboard;
using MyStock.Application.Products;
using MyStock.Application.Purchases;
using MyStock.Application.Sale;
using MyStock.Application.Settings;
using MyStock.Application.Uoms;
using MyStock.Application.Vendors;
using MyStock.Core.Interfaces;
using System.Reactive.Concurrency;
using System.Windows.Input;
using System.Windows.Media;

namespace MyStock.ViewModels;

public class MainViewModel : ReactiveObject
{
    private IEntityListPage contentControl;
    private Common.MenuItem selectedMenu;

    public IEntityListPage SelectedPage { get => contentControl; set => this.RaiseAndSetIfChanged(ref contentControl, value); }
    public Common.MenuItem SelectedMenu { get => selectedMenu; set => this.RaiseAndSetIfChanged(ref selectedMenu, value); }

    public MainViewModel()
    {
        Router = new RoutingState();
        BuildMenu();
    }

    public ICommand Navigate => ReactiveCommand.Create<Type>(param =>
    {
        if (param is Type menu)
        {
            if (SelectedPage == null || menu.Name != SelectedPage.ViewModel.GetType().Name)
            {
                var viewModel = Global.Container.Resolve(menu, new PositionalParameter(0, Global.Container.Resolve<IContext>())) as INavigatable;
                SelectedPage = viewModel.EntityPage;
            }
        }
    }, outputScheduler: Scheduler.CurrentThread);

    public List<Common.MenuItem> Menu { get; set; }
    public List<Common.MenuItem> OptionsMenu { get; set; }

    public RoutingState Router { get; }

    void BuildMenu()
    {
        Menu = new List<Common.MenuItem>();
        Menu.Add(new Common.MenuItem(typeof(IDashboardViewModel), "Dashboard", "Home", 1, ""));
        Menu.Add(new Common.MenuItem(typeof(ISalesListViewModel), Translations.Sales, "CartArrowUp", 5, ""));
        Menu.Add(new Common.MenuItem(typeof(IPurchaseListViewModel), Translations.Purchases, "CartArrowDown", 6, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), Translations.UOMs, "Ruler", 3, ""));
        Menu.Add(new Common.MenuItem(typeof(IProductListViewModel), Translations.Products, "PackageVariant", 2, ""));
        Menu.Add(new Common.MenuItem(typeof(ICustomerListViewModel), Translations.Customers, "Account", 7, ""));
        Menu.Add(new Common.MenuItem(typeof(IVendorListViewModel), Translations.Vendors, "AccountTie", 8, ""));
        Menu.ForEach(m =>
        {
            m.Command = Navigate;
            m.CommandParameter = m.ViewModelType;
        });

        OptionsMenu = new List<Common.MenuItem>();
        //OptionsMenu.Add(new Common.MenuItem(typeof(IVendorListViewModel), "Profile", "AccountCog", 1, ""));
        OptionsMenu.Add(new Common.MenuItem(typeof(ISettingsViewModel), "Options", "Cog", 2, ""));
        OptionsMenu.ForEach(m =>
        {
            m.Command = Navigate;
            m.CommandParameter = m.ViewModelType;
        });

        SelectedMenu = Menu[0];
    }
}