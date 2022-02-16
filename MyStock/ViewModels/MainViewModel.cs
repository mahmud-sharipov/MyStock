using Autofac;
using MyStock.Application.Assets.Lang;
using MyStock.Application.Customers;
using MyStock.Application.Products;
using MyStock.Application.Sale;
using MyStock.Application.Uoms;
using MyStock.Application.Vendors;
using MyStock.Core.Interfaces;
using System.Reactive.Concurrency;
using System.Windows.Input;

namespace MyStock.ViewModels;

public class MainViewModel : ReactiveObject
{
    private IEntityListPage contentControl;
    public IEntityListPage SelectedPage
    {
        get => contentControl;
        set => this.RaiseAndSetIfChanged(ref contentControl, value);
    }

    public MainViewModel()
    {
        Router = new RoutingState();
        BuildMenu();
    }

    public ICommand Navigate => ReactiveCommand.Create<Type>(param =>
    {
        if (param is Type menu)
        {
            var viewModel = Global.Container.Resolve(menu, new PositionalParameter(0, Global.Container.Resolve<IContext>())) as INavigatable;
            SelectedPage = viewModel.EntityPage;
        }
    }, outputScheduler: Scheduler.CurrentThread);

    public List<Common.MenuItem> Menu { get; set; }
    public List<Common.MenuItem> OptionsMenu { get; set; }

    public RoutingState Router { get; }

    void BuildMenu()
    {
        Menu = new List<Common.MenuItem>();
        OptionsMenu = new List<Common.MenuItem>();
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), "Dashboard", "Home", 1, ""));
        Menu.Add(new Common.MenuItem(typeof(IProductListViewModel), Translations.Products, "PackageVariant", 2, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), Translations.UOMs, "Ruler", 3, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), Translations.UOMs, "FormatListChecks", 4, ""));
        Menu.Add(new Common.MenuItem(typeof(ISalesListViewModel), Translations.Sales, "ClipboardList", 5, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), Translations.Purchases, "Cart", 6, ""));
        Menu.Add(new Common.MenuItem(typeof(ICustomerListViewModel), Translations.Customers, "AccountGroup", 7, ""));
        Menu.Add(new Common.MenuItem(typeof(IVendorListViewModel), Translations.Vendors, "AccountBox", 8, ""));
        OptionsMenu.Add(new Common.MenuItem(typeof(IVendorListViewModel), "Profile", "AccountCog", 1, ""));
        OptionsMenu.Add(new Common.MenuItem(typeof(IVendorListViewModel), "Options", "Cog", 2, ""));
        Menu.ForEach(m =>
        {
            m.Command = Navigate;
            m.CommandParameter = m.ViewModelType;
        });
        OptionsMenu.ForEach(m =>
        {
            m.Command = Navigate;
            m.CommandParameter = m.ViewModelType;
        });
    }
}