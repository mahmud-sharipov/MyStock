using MyStock.Application.Sale;
using MyStock.Application.Sale.Pages;

namespace MyStock.Pages.Sale
{
    public class SalesListPage : EntityListPage<SalesListViewModel>, ISalesListEntityPage
    {
        public SalesListPage(SalesListViewModel viewModel) : base(viewModel)
        {
            //var filter = new UomListFilter();
            //filter.NameSearchTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(ViewModel.NameSearchText)) { Source = ViewModel, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            //ColletionFilters = filter;
            InitializeDefaulPage();
        }
    }
}
