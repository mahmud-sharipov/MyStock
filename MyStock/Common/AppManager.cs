using System.Globalization;
using System.Threading;

namespace MyStock.Common;

public static class AppManager
{
    private static readonly PaletteHelper _paletteHelper = new PaletteHelper();
    public static UISettings _uiSettings;

    public static Domain.Settings Settings => Global.Settings;

    public static UISettings UISettings =>
        _uiSettings ??= JsonConvert.DeserializeObject<UISettings>(Settings.UISettings ?? "") ?? new UISettings();

    public static CultureInfo CultureInfo { get; private set; }

    public static void Start()
    {
        CultureInfo = new CultureInfo(Global.Settings.Lagnuage)
        {
            NumberFormat = new NumberFormatInfo()
            {
                CurrencySymbol = "сом.",
                CurrencyPositivePattern = 3,
                CurrencyNegativePattern = 8
            }
        };
        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo;
        SetTheme(UISettings.Theme);
    }

    public static void Stop()
    {
        var settings = JsonConvert.SerializeObject(UISettings);
        Settings.UISettings = settings;
        Global.Context.SaveChanges();
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
        ITheme newTheme = _paletteHelper.GetTheme();
        newTheme.SetBaseTheme(Theme.Light);

        if (theme == UITheme.Dark)
            newTheme.SetBaseTheme(Theme.Dark);

        _paletteHelper.SetTheme(newTheme);
    }
}

public static class AppConfig
{
    public static double FontSize => AppManager.UISettings.AppFontSize;
    public static double Header1FontSize => AppManager.UISettings.AppFontSize + 10;
    public static double Header2FontSize => AppManager.UISettings.AppFontSize + 8;
    public static double Header3FontSize => AppManager.UISettings.AppFontSize + 6;
    public static double Header4FontSize => AppManager.UISettings.AppFontSize + 4;
    public static double Header5FontSize => AppManager.UISettings.AppFontSize + 2;
    public static string CompanyName => AppManager.Settings.CompanyName;
}

public enum UITheme
{
    Light = 0,
    Dark = 1
}