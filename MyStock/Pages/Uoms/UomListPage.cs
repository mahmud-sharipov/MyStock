using MyStock.Application.Uoms;
using MyStock.Application.Uoms.Pages;
using System.Windows.Data;

namespace MyStock.Pages.Uoms
{
    public class UomListPage : EntityListPage<UomListViewModel>, IUomListEntityPage
    {
        public UomListPage(UomListViewModel viewModel) : base(viewModel)
        {
            var filter = new UomListFilter();
            filter.NameSearchTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(ViewModel.NameSearchText)) { Source = ViewModel, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            HeaderContent = filter;
            InitializeDefaulPage();
        }
    }
}
