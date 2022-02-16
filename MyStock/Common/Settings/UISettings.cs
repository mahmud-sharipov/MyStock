namespace MyStock.Common.Settings;
public class UISettings
{
    public UITheme Theme { get; set; } = UITheme.Light;
    public string PrimaryColor { get; set; } = "Blue";
    public string SecondaryColor { get; set; } = "Orange";
    public double AppFontSize { get; set; } = 12;
}
