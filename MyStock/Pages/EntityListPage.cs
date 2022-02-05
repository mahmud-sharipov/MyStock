using MyStock.Core.Interfaces;
using ReactiveUI;
using Syncfusion.UI.Xaml.Grid;
using System.Windows.Data;

namespace MyStock.Pages;

public class EntityListPage<TViewModel> : BasePage<TViewModel>, IEntityListPage
    where TViewModel : class, IEntityListPageViewModel
{
    public EntityListPage(TViewModel viewModel) : base(viewModel)
    {

    }

    INavigatable IEntityListPage.ViewModel
    {
        get => ViewModel;
        set => ViewModel = value as TViewModel;
    }

    protected void SetupCollection(SfDataGrid dataGrid)
    {
        dataGrid.Columns.Clear();
        dataGrid.AutoGenerateColumns = false;
        dataGrid.SearchHelper.AllowFiltering = true;
        dataGrid.SearchHelper.CanHighlightSearchText = true;
        Binding myBinding = new Binding(nameof(ViewModel.Collection));
        myBinding.Source = ViewModel;
        dataGrid.SetBinding(SfDataGrid.ItemsSourceProperty, myBinding);
        foreach (var column in ViewModel.Columns)
        {
            dataGrid.Columns.Add(new GridTextColumn()
            {
                MappingName = column.BindingPath,
                HeaderText = column.Label,
                AllowEditing = false,
                AllowFiltering = column.AllowFilter
            });
        }
    }
}