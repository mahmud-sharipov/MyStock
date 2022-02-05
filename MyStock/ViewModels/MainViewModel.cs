using Autofac;
using MyStock.Application.Uoms;
using MyStock.Core.Interfaces;
using MyStock.Pages.Uoms;
using ReactiveUI;
using System.Linq;
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

    public RoutingState Router { get; }

    void BuildMenu()
    {
        Menu = new List<Common.MenuItem>();
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), "Dashboard", "Home", 1, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), "Products", "PackageVariant", 2, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), "UOMs", "Ruler", 3, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), "Orders", "FormatListChecks", 4, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), "Sales", "ClipboardList", 5, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), "Purchases", "Cart", 6, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), "Customers", "AccountGroup", 7, ""));
        Menu.Add(new Common.MenuItem(typeof(IUomListViewModel), "Vendors", "AccountGroup", 8, ""));
        Menu.ForEach(m =>
        {
            m.Command = Navigate;
            m.CommandParameter = m.ViewModelType;
        });
    }
}