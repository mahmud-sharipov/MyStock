using System.Windows.Data;

namespace MyStock.Controls;

public abstract partial class FullTextSearch : UserControl
{
    public string Hint
    {
        get { return (string)GetValue(HintProperty); }
        set { SetValue(HintProperty, value); }
    }

    public static readonly DependencyProperty HintProperty =
        DependencyProperty.Register("Hint", typeof(string), typeof(FullTextSearch), new PropertyMetadata(string.Empty));

    public bool ShowSubtitle
    {
        get { return (bool)GetValue(ShowSubtitleProperty); }
        set { SetValue(ShowSubtitleProperty, value); }
    }

    public static readonly DependencyProperty ShowSubtitleProperty =
        DependencyProperty.Register("ShowSubtitle", typeof(bool), typeof(FullTextSearch), new PropertyMetadata(true));

    public FullTextSearch()
    {
        InitializeComponent();
        Loaded += FullTextSearch_Loaded;
    }

    private void FullTextSearch_Loaded(object sender, EventArgs e)
    {
        ResultList.ItemTemplate = GetListItemTemplate();
        Loaded -= FullTextSearch_Loaded;
    }

    private void uiSearch_GotFocus(object sender, RoutedEventArgs e)
    {
        resultPopup.IsOpen = true;
    }

    private void uiSearch_LostFocus(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(uiSearch.Text))
            ResultList.SelectedItem = null;
        else if (uiSearch.Text != ResultList.SelectedItem?.ToString())
        {
            foreach (var item in ResultList.ItemsSource)
            {
                if (item == ResultList.SelectedItem)
                    uiSearch.Text = item.ToString();
                else
                    ResultList.SelectedItem = item;
                break;
            }
        }
        resultPopup.IsOpen = false;
    }

    protected virtual DataTemplate GetListItemTemplate()
    {
        var grid = new FrameworkElementFactory(typeof(Grid));
        grid.SetValue(Grid.MarginProperty, new Thickness(0, 0, 0, 2));
        grid.SetBinding(Grid.WidthProperty, new Binding(nameof(ActualWidth)) { RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(ListBox), 1) });

        FrameworkElementFactory col1 = new FrameworkElementFactory(typeof(ColumnDefinition));
        FrameworkElementFactory col2 = new FrameworkElementFactory(typeof(ColumnDefinition));
        col1.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Auto));
        col2.SetValue(ColumnDefinition.WidthProperty, new GridLength(1, GridUnitType.Star));
        grid.AppendChild(col1);
        grid.AppendChild(col2);

        var iconFactory = GetTileIconTemplateFactory();
        iconFactory.SetValue(Grid.ColumnProperty, 0);
        grid.AppendChild(iconFactory);
        var tileContentFactory = GetTileTemplateFactory();
        tileContentFactory.SetValue(Grid.ColumnProperty, 1);
        grid.AppendChild(tileContentFactory);

        var border = new FrameworkElementFactory(typeof(Border));
        border.SetResourceReference(Border.BorderBrushProperty, "MaterialDesignDivider");
        border.SetValue(Border.BorderThicknessProperty, new Thickness(0,0,0, 1));
        border.AppendChild(grid);
        return new DataTemplate() { VisualTree = border };
    }

    protected virtual FrameworkElementFactory GetTileIconTemplateFactory()
    {
        var icon = new FrameworkElementFactory(typeof(PackIconMaterial));
        icon.SetValue(PackIconMaterial.WidthProperty, 20.0);
        icon.SetValue(PackIconMaterial.HeightProperty, 20.0);
        icon.SetValue(PackIconMaterial.HorizontalAlignmentProperty, HorizontalAlignment.Center);
        icon.SetValue(PackIconMaterial.VerticalAlignmentProperty, VerticalAlignment.Center);
        icon.SetValue(PackIconMaterial.KindProperty, GetTileIcon());
        return icon;
    }

    protected virtual FrameworkElementFactory GetTileTemplateFactory()
    {
        var grid = new FrameworkElementFactory(typeof(Grid));
        grid.SetValue(Grid.MarginProperty, new Thickness(10, 0, 0, 0));
        var row1 = new FrameworkElementFactory(typeof(RowDefinition));
        row1.SetValue(RowDefinition.HeightProperty, new GridLength(1, GridUnitType.Auto));
        var row2 = new FrameworkElementFactory(typeof(RowDefinition));
        row2.SetValue(RowDefinition.HeightProperty, new GridLength(1, GridUnitType.Auto));
        grid.AppendChild(row1);
        grid.AppendChild(row2);

        var titleTextBlock = new FrameworkElementFactory(typeof(TextBlock));
        titleTextBlock.SetValue(Grid.RowProperty, 0);
        titleTextBlock.SetValue(TextBlock.FontSizeProperty, AppManager.UISettings.AppFontSize);
        titleTextBlock.SetValue(TextBlock.FontWeightProperty, FontWeights.Medium);
        titleTextBlock.SetBinding(TextBlock.TextProperty, new Binding(GetTitlePropertyName()));
        grid.AppendChild(titleTextBlock);

        if (ShowSubtitle)
        {
            var subtitleTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            subtitleTextBlock.SetValue(Grid.RowProperty, 1);
            subtitleTextBlock.SetValue(TextBlock.FontSizeProperty, AppManager.UISettings.AppFontSize - 2);
            subtitleTextBlock.SetValue(TextBlock.MarginProperty, new Thickness(0, 3, 0, 0));
            subtitleTextBlock.SetBinding(TextBlock.TextProperty, new Binding(GetSubtitlePropertyName()));
            grid.AppendChild(subtitleTextBlock);
        }
        return grid;
    }

    protected abstract string GetTitlePropertyName();
    protected abstract string GetSubtitlePropertyName();
    protected abstract PackIconMaterialKind GetTileIcon();
}