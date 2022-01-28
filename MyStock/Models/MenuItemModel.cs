namespace MyStock.Models;

public class MenuItemModel
{
    public MenuItemModel() : this(string.Empty, string.Empty, string.Empty, 1, string.Empty) { }

    public MenuItemModel(string name, string icon, string uri, int order, string description)
    {
        Name = name;
        Icon = icon;
        Order = order;
        Description = description;
        Url = new Uri(uri, UriKind.Relative);
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Icon { get; set; }
    public int Order { get; set; }
    public Uri Url { get; set; }
}
