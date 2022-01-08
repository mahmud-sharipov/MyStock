namespace MyStock.Models;

public class MenuItemModel
{
    public MenuItemModel()
    {
        Name = String.Empty;
        Description = String.Empty;
        Icon = String.Empty;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public Uri? Url { get; set; }
}
