using System.ComponentModel;

namespace MyStock.Controls;

public partial class Logo : UserControl, INotifyPropertyChanged
{
    public Logo()
    {
        ItemsMargin = new Thickness(Thickness);
        DataContext = this;
        InitializeComponent();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private Thickness _itemsMargin;

    public Thickness ItemsMargin
    {
        get => _itemsMargin;
        set
        {
            _itemsMargin = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Logo.ItemsMargin)));
        }
    }

    #region ThicknessProperty
    public double Thickness
    {
        get { return (double)GetValue(ThicknessProperty); }
        set { SetValue(ThicknessProperty, value); }
    }

    public static readonly DependencyProperty ThicknessProperty =
        DependencyProperty.Register("Thickness", typeof(double), typeof(Logo), new PropertyMetadata(5.0, OnCurrentTimePropertyChanged));
    private static void OnCurrentTimePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs e)
    {
        var logo = (Logo)source;
        if (logo != null)
            logo.ItemsMargin = new Thickness((double)e.NewValue);
    }
    #endregion
}
