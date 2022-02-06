using Autofac;
using FluentValidation;
using MyStock.Application.Products.Pages;
using MyStock.Application.Uoms.Pages;
using MyStock.Core.Interfaces;
using MyStock.IoC;
using MyStock.Pages.Products;
using MyStock.Pages.Uoms;
using MyStock.Persistence.Seed;
using System.Windows.Navigation;

namespace MyStock;

public partial class App : System.Windows.Application
{
    public App()
    {
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTc2NTk3QDMxMzkyZTM0MmUzMEFJRlNrTlBnWXArTVRHVFc3K3J4Y1hTMUR5NzIrOVpyeDFTSEplbTFtdXc9");
        ShutdownMode = ShutdownMode.OnLastWindowClose;
    }

    public static IContainer Container { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        var builder = new ContainerBuilder();
        NativeInjectorBootStrapper.RegisterServices(builder);
        RegisterClientIoC(builder);
        Container = builder.Build();
        Global.Container = Container;
        GlobalContext = Container.Resolve<IContext>();
        AppManager.Start();
        SeedDatabase.Seed(GlobalContext);
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppManager.Stop();
        base.OnExit(e);
    }

    public static IContext GlobalContext { get; private set; }

    public static void RegisterClientIoC(ContainerBuilder builder)
    {
        builder.RegisterType<Common.DialogHost>().As<IDialogHost>().SingleInstance();
        builder.RegisterType<UomListPage>().As<IUomListEntityPage>().InstancePerDependency();
        builder.RegisterType<UomPage>().As<IUomEntityPage>().InstancePerDependency();

        builder.RegisterType<ProductListPage>().As<IProductListEntityPage>().InstancePerDependency();
        builder.RegisterType<ProductPage>().As<IProductEntityPage>().InstancePerDependency();
    }
}
