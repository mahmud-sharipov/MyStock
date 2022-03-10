using MyStock.Application.Sale;
using MyStock.Application.Sale.Pages;

namespace MyStock.Pages.Sale
{
    public class SalesListPage : EntityListPage<SalesListViewModel>, ISalesListEntityPage
    {
        public SalesListPage(SalesListViewModel viewModel) : base(viewModel)
        {
            HeaderContent = new SalesListFilter() { DataContext = viewModel }; ;
            InitializeDefaulPage();
        }
    }
}
