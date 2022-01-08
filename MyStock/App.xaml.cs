namespace MyStock;

public partial class App : Application
{
    public App()
    {
        ShutdownMode = ShutdownMode.OnLastWindowClose;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        AppMananger.Start();
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppMananger.Stop();
        base.OnExit(e);
    }
}
