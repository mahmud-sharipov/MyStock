using MyStock.Application.Purchases;
using MyStock.Application.Purchases.Pages;

namespace MyStock.Pages.Purchases
{
    public class PurchaseListPage : EntityListPage<PurchaseListViewModel>, IPurchaseListEntityPage
    {
        public PurchaseListPage(PurchaseListViewModel viewModel) : base(viewModel)
        {
            //var filter = new UomListFilter();
            //filter.NameSearchTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(ViewModel.NameSearchText)) { Source = ViewModel, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            //ColletionFilters = filter;
            InitializeDefaulPage();
        }
    }
}
