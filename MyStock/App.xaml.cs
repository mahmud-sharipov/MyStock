using Autofac;
using MyStock.Application.Uoms.Pages;
using MyStock.Core.Interfaces;
using MyStock.IoC;
using MyStock.Pages.Uoms;

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

    }
}
