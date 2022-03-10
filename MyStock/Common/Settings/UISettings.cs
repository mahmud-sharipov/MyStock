using System.Windows.Media;

namespace MyStock.Common.Settings;
public class UISettings
{
    public UITheme Theme { get; set; } = UITheme.Light;
    public Color? Background { get; set; }
    public Color? Foreground { get; set; }
    public string SecondaryColor { get; set; } = "Orange";
    public double AppFontSize { get; set; } = 12;
    public bool IsCustomColor { get; set; } = false;
}
