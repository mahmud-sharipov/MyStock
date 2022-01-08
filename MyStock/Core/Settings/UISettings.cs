namespace MyStock.Core.Settings;
public class UISettings
{
    public UITheme Theme { get; set; } = UITheme.Light;
    public string PrimaryColor { get; set; } = "Blue";
    public string SecondaryColor { get; set; } = "Orange";
}
