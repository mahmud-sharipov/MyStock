using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using System.Globalization;
using System.Threading;
using System.Windows.Markup;
using System.Windows.Media;

namespace MyStock.Common;

public static class AppManager
{
    internal static readonly PaletteHelper _paletteHelper = new PaletteHelper();
    static UISettings _uiSettings;

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
                CurrencySymbol = "с.",
                CurrencyPositivePattern = 3,
                CurrencyNegativePattern = 8
            }
        };
        CultureInfo.DateTimeFormat.ShortDatePattern = Global.DateFormate;
        Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = CultureInfo;
        FrameworkElement.LanguageProperty.OverrideMetadata(typeof(FrameworkElement), new FrameworkPropertyMetadata(
            XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
        SetTheme(UISettings.Theme);
        if (UISettings.Background != null)
            ChangeColor(UISettings.Background.Value, UISettings.Foreground);
    }

    public static void Stop()
    {
        var settings = JsonConvert.SerializeObject(UISettings);
        Settings.UISettings = settings;
        Global.Context.SaveChanges();
    }

    public static void SetTheme(UITheme theme)
    {
        ITheme newTheme = _paletteHelper.GetTheme();

        if (theme == UITheme.Dark)
            newTheme.SetBaseTheme(Theme.Dark);
        else
            newTheme.SetBaseTheme(Theme.Light);

        UISettings.Theme = theme;
        _paletteHelper.SetTheme(newTheme);
    }

    public static void ChangeColor(Color background, Color? foreground = null)
    {
        ITheme theme = _paletteHelper.GetTheme();

        theme.PrimaryLight = theme.SecondaryLight = new ColorPair(background.Lighten(), foreground);
        theme.PrimaryMid = theme.SecondaryMid = new ColorPair(background, foreground);
        theme.PrimaryDark = theme.SecondaryDark = new ColorPair(background.Darken(), foreground);

        _paletteHelper.SetTheme(theme);
        UISettings.Background = background;
        UISettings.Foreground = theme.PrimaryMid.GetForegroundColor();
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
    public static string DateFormat => Global.DateFormate;
}

public enum UITheme
{
    Light = 0,
    Dark = 1
}