using MyStock.Application.Assets.Lang;
using MyStock.Core.Interfaces;
using System.Windows.Data;
using System.Windows.Input;

namespace MyStock.Pages;

public class EntityListPage<TViewModel> : BasePage<TViewModel>, IEntityListPage
    where TViewModel : class, IEntityListPageViewModel
{
    public DataGrid CollectionDataGrid { get; set; }
    public TextBlock TitleBlock { get; set; }
    public UserControl ColletionFilters { get; set; }

    public EntityListPage(TViewModel viewModel) : base(viewModel)
    {
        Loaded += OnPageLoad;
    }

    private void OnPageUnloaded(object sender, RoutedEventArgs e)
    {
        Unloaded -= OnPageUnloaded;
        Loaded -= OnPageLoad;
        ViewModel?.Dispose();
        ViewModel = null;
        IsDisposed = true;
    }

    private void OnPageLoad(object sender, RoutedEventArgs e)
    {
        Unloaded += OnPageUnloaded;
    }

    INavigatable IEntityListPage.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as TViewModel;
    }

    #region Auto generate
    protected TextBlock InitTitle()
    {
        TitleBlock = new TextBlock()
        {
            Margin = new Thickness(0, 15, 0, 10),
            Style = System.Windows.Application.Current.FindResource("MaterialDesignHeadline5TextBlock") as Style
        };
        TitleBlock.SetBinding(TextBlock.TextProperty, new Binding(nameof(ViewModel.Title)) { Source = ViewModel });

        return TitleBlock;
    }

    protected Grid InitTitlePanel()
    {
        var gridPanel = new Grid();
        gridPanel.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        gridPanel.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        gridPanel.Children.Add(InitTitle());

        if (ColletionFilters != null)
        {
            ColletionFilters.SetValue(Grid.RowProperty, 1);
            gridPanel.Children.Add(ColletionFilters);
        }
        return gridPanel;
    }

    protected DataGrid InitCollectionDataGrid()
    {
        CollectionDataGrid = new DataGrid();
        CollectionDataGrid.SetResourceReference(DataGrid.ForegroundProperty, "MaterialDesignBody");
        SetupCollection(CollectionDataGrid);
        return CollectionDataGrid;
    }

    protected Button InitAddButtin()
    {
        var addButton = new Button()
        {
            Margin = new Thickness(0, 0, 10, 10),
            HorizontalAlignment = HorizontalAlignment.Right,
            VerticalAlignment = VerticalAlignment.Bottom,
            Style = System.Windows.Application.Current.FindResource("MaterialDesignFloatingActionDarkButton") as Style,
            ToolTip = Translations.Add
        };
        addButton.SetBinding(Button.CommandProperty, new Binding(nameof(ViewModel.Add)) { Source = ViewModel });
        addButton.Content = new PackIcon() { Width = 30, Height = 30, Kind = PackIconKind.Plus };
        return addButton;
    }

    protected void InitializeDefaulPage()
    {
        var mainGrid = new Grid();
        mainGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        mainGrid.RowDefinitions.Add(new RowDefinition());
        mainGrid.Children.Add(InitTitlePanel());

        var cardGrig = new Grid();
        cardGrig.Children.Add(InitCollectionDataGrid());
        cardGrig.Children.Add(InitAddButtin());

        var card = new Card();
        card.Margin = new Thickness(0, 10, 0, 10);
        card.SetValue(Grid.RowProperty, 1);
        card.Content = cardGrig;
        mainGrid.Children.Add(card);
        Content = mainGrid;
    }

    protected void SetupCollection(DataGrid dataGrid)
    {
        dataGrid.Columns.Clear();
        dataGrid.AutoGenerateColumns = false;
        dataGrid.CanUserAddRows = false;
        dataGrid.CanUserDeleteRows = false;
        dataGrid.SetBinding(DataGrid.ItemsSourceProperty, new Binding(nameof(ViewModel.Collection)) { Source = ViewModel });
        dataGrid.SetBinding(DataGrid.SelectedItemProperty, new Binding(nameof(ViewModel.SelectedItem)) { Source = ViewModel });
        dataGrid.InputBindings.Add(new KeyBinding(ViewModel.Open, Key.O, ModifierKeys.Control));
        foreach (var column in ViewModel.Columns)
        {
            dataGrid.Columns.Add(new MaterialDesignThemes.Wpf.DataGridTextColumn()
            {
                Header = column.Label,
                Binding = new Binding(column.BindingPath),
                IsReadOnly = true,
            });
        }
        CreateActionsColumn(dataGrid);
    }

    void CreateActionsColumn(DataGrid dataGrid)
    {
        DataGridTemplateColumn col = new DataGridTemplateColumn();
        var sp = new FrameworkElementFactory(typeof(StackPanel));
        sp.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);

        var style = System.Windows.Application.Current.FindResource("MaterialDesignFlatButton") as Style;

        sp.AppendChild(CreateButtonFactory(style, PackIconKind.Pencil,
            new Binding(nameof(ViewModel.Open)) { Source = ViewModel },
            new Binding(nameof(ViewModel.SelectedItem)) { Source = ViewModel })
        );

        sp.AppendChild(CreateButtonFactory(style, PackIconKind.Delete,
            new Binding(nameof(ViewModel.Delete)) { Source = ViewModel },
            new Binding("DataContext") { RelativeSource = new RelativeSource(RelativeSourceMode.Self) })
        );

        col.CellTemplate = new DataTemplate() { VisualTree = sp };
        dataGrid.Columns.Add(col);

    }

    private FrameworkElementFactory CreateButtonFactory(Style style, PackIconKind icon, Binding commandBinding, Binding commandParameterBinding)
    {
        var factory = new FrameworkElementFactory(typeof(Button));
        factory.SetValue(Button.StyleProperty, style);
        factory.SetValue(Button.PaddingProperty, new Thickness(5, 0, 5, 0));
        factory.SetBinding(Button.CommandProperty, commandBinding);
        factory.SetBinding(Button.CommandParameterProperty, commandParameterBinding);
        factory.AppendChild(CreateIconFactory(icon));
        return factory;
    }

    private FrameworkElementFactory CreateIconFactory(PackIconKind kind)
    {
        var iconFactory = new FrameworkElementFactory(typeof(PackIcon));
        iconFactory.SetValue(PackIcon.KindProperty, kind);
        iconFactory.SetValue(PackIcon.WidthProperty, 20.0);
        iconFactory.SetValue(PackIcon.HeightProperty, 20.0);
        return iconFactory;
    }
    #endregion
}