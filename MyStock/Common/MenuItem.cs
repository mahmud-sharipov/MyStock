using MahApps.Metro.Controls;
using System.ComponentModel;

namespace MyStock.Common;

public class MenuItem : HamburgerMenuIconItem, IReactiveObject
{
    public MenuItem(Type viewModelType, string label, string icon, int order, string toolTip)
    {
        Label = label;
        Icon = icon;
        Order = order;
        ToolTip = toolTip;
        ViewModelType = viewModelType;
    }

    public int Order { get; set; }

    public Type ViewModelType { get; set; }

    #region IReactiveObject

    public event PropertyChangedEventHandler PropertyChanged;
    public event PropertyChangingEventHandler PropertyChanging;
    public void RaisePropertyChanged(PropertyChangedEventArgs args)
    {
        PropertyChanged?.Invoke(this, args);
    }

    public void RaisePropertyChanging(PropertyChangingEventArgs args)
    {
        PropertyChanging?.Invoke(this, args);
    }

    #endregion
}