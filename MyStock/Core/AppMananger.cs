using System.Reflection;

namespace MyStock.Core;

public class AppMananger
{
    private static readonly object padlock = new object();
    private static AppMananger? instance;
    private readonly PaletteHelper paletteHelper = new PaletteHelper();
    private readonly string _settingsPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? "", @"Assets/Settings/appsettings.json");

    AppMananger()
    {
        AppSettings = JsonConvert.DeserializeObject<AppSettings>(File.ReadAllText(_settingsPath)) ?? new AppSettings();
    }

    public static AppMananger Instance
    {
        get
        {
            lock (padlock)
            {
                if (instance == null)
                    instance = new AppMananger();
                return instance;
            }
        }
    }

    public static AppSettings Settings => Instance.AppSettings;
    public static UISettings UISettings => Instance.AppSettings.UISettings;
    AppSettings AppSettings { get; init; }

    public static void Start()
    {
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(Settings.Language);
        System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo(Settings.Language);
        SetTheme(UISettings.Theme);
    }

    public static void Stop()
    {
        var settings = JsonConvert.SerializeObject(Settings);
        File.WriteAllText(Instance._settingsPath, settings);
    }

    public static void ChangeTheme(UITheme theme)
    {
        if (theme != UISettings.Theme)
        {
            SetTheme(theme);
            UISettings.Theme = theme;
        }
    }

    public static void SetTheme(UITheme theme)
    {
        ITheme newTheme = Instance.paletteHelper.GetTheme();
        newTheme.SetBaseTheme(Theme.Light);

        if (theme == UITheme.Dark)
            newTheme.SetBaseTheme(Theme.Dark);

        Instance.paletteHelper.SetTheme(newTheme);
    }
}

public enum UITheme
{
    Light = 0,
    Dark = 1
}