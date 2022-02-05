using Autofac;
using MahApps.Metro.Controls;
using MyStock.Core.Interfaces;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Concurrency;

namespace MyStock.Common;

public class MenuItem : HamburgerMenuIconItem, IReactiveObject
{
    private IEntityListPage _content;

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