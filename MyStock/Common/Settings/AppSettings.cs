namespace MyStock.Common.Settings;

public class AppSettings
{
    public AppSettings()
    {
        Language = "ru-RU";
        UISettings = new UISettings();
    }

    public string Language { get; set; }
    public UISettings UISettings { get; set; }
}