namespace MyStock;

public partial class App : Application
{
    public App()
    {
        ShutdownMode = ShutdownMode.OnLastWindowClose;
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        AppManager.Start();
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        AppManager.Stop();
        base.OnExit(e);
    }
}
