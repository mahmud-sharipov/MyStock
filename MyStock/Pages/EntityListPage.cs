using MyStock.Application.Assets.Lang;
using MyStock.Core.Interfaces;
using MyStock.Templates;
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
    protected void InitializeDefaulPage()
    {
        var tempPage = new EntityListPageTemplate() { DataContext = ViewModel };
        CollectionDataGrid = tempPage.EntityListGrid;
        SetupCollection(CollectionDataGrid);
        Content = tempPage;
    }

    protected void SetupCollection(DataGrid dataGrid)
    {
        dataGrid.Columns.Clear();
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