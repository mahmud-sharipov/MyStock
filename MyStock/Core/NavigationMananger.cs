namespace MyStock.Core;
public class NavigationMananger
{
    private static readonly object padlock = new object();
    private static NavigationMananger? instance;

    NavigationMananger()
    {
        MainFrame = new Frame();
    }

    public Frame MainFrame { get; }

    public NavigationService NavigationService => Instance.MainFrame.NavigationService;

    public static NavigationMananger Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new NavigationMananger();
                return instance;
            }
        }
    }
}