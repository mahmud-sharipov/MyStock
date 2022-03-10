using MyStock.Application.Purchases;
using MyStock.Application.Purchases.Pages;
using MyStock.Pages.Sale;

namespace MyStock.Pages.Purchases
{
    public class PurchaseListPage : EntityListPage<PurchaseListViewModel>, IPurchaseListEntityPage
    {
        public PurchaseListPage(PurchaseListViewModel viewModel) : base(viewModel)
        {
            HeaderContent = new SalesListFilter() { DataContext = viewModel }; ;
            InitializeDefaulPage();
        }
    }
}
