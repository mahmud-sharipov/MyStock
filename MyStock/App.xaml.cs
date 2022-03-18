using Autofac;
using MyStock.Application.Assets.Lang;
using MyStock.Application.Category;
using MyStock.Application.Customers.Pages;
using MyStock.Application.Dashboard;
using MyStock.Application.Products.Pages;
using MyStock.Application.Purchases.Pages;
using MyStock.Application.Sale.Pages;
using MyStock.Application.Settings;
using MyStock.Application.Uoms.Pages;
using MyStock.Application.Vendors.Pages;
using MyStock.Core.Interfaces;
using MyStock.IoC;
using MyStock.Pages.Customers;
using MyStock.Pages.Dashboard;
using MyStock.Pages.Products;
using MyStock.Pages.Purchases;
using MyStock.Pages.Sale;
using MyStock.Pages.Settings;
using MyStock.Pages.Uoms;
using MyStock.Pages.Vendors;
using MyStock.Persistence.Seed;
using System.Diagnostics;
using System.Linq;

namespace MyStock;

public partial class App : System.Windows.Application
{
    public App()
    {
        ShutdownMode = ShutdownMode.OnLastWindowClose;
    }

    public static IContainer Container { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        if (Process.GetProcesses().Where(p => p.ProcessName == Process.GetCurrentProcess().ProcessName).Count() > 1)
            App.Current.Shutdown();

        Dispatcher.UnhandledException += Dispatcher_UnhandledException;

        var builder = new ContainerBuilder();
        NativeInjectorBootStrapper.RegisterServices(builder);
        RegisterClientIoC(builder);
        Container = builder.Build();
        Global.Container = Container;
        SeedDatabase.Seed(Global.Context);

        AppManager.Start();
        base.OnStartup(e);
    }

    private void Dispatcher_UnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(e.Exception.Message);
        e.Handled = true;
        App.Current.Shutdown();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppManager.Stop();
        base.OnExit(e);
    }

    public static void RegisterClientIoC(ContainerBuilder builder)
    {
        builder.RegisterType<Common.DialogHost>().As<IDialogHost>().SingleInstance();

        builder.RegisterType<UomListPage>().As<IUomListEntityPage>().InstancePerDependency();
        builder.RegisterType<UomPage>().As<IUomEntityPage>().InstancePerDependency();

        builder.RegisterType<ProductListPage>().As<IProductListEntityPage>().InstancePerDependency();
        builder.RegisterType<ProductPage>().As<IProductEntityPage>().InstancePerDependency();
        builder.RegisterType<ProductCategoryPage>().As<IProductCategoryEntityPage>().InstancePerDependency();

        builder.RegisterType<CustomerListPage>().As<ICustomerListEntityPage>().InstancePerDependency();
        builder.RegisterType<CustomerPage>().As<ICustomerEntityPage>().InstancePerDependency();

        builder.RegisterType<VendorListPage>().As<IVendorListEntityPage>().InstancePerDependency();
        builder.RegisterType<VendorPage>().As<IVendorEntityPage>().InstancePerDependency();

        builder.RegisterType<SalesListPage>().As<ISalesListEntityPage>().InstancePerDependency();
        builder.RegisterType<SalesPage>().As<ISalesEntityPage>().InstancePerDependency();

        builder.RegisterType<PurchaseListPage>().As<IPurchaseListEntityPage>().InstancePerDependency();
        builder.RegisterType<PurchasePage>().As<IPurchaseEntityPage>().InstancePerDependency();

        builder.RegisterType<DashboardPage>().As<IDashboardPage>().InstancePerDependency();
        builder.RegisterType<SettingsPage>().As<ISettingsPage>().InstancePerDependency();
    }
}
