using MyStock.Application.Products;
using MyStock.Application.Products.Pages;

namespace MyStock.Pages.Products
{
    public class ProductListPage : EntityListPage<ProductListViewModel>, IProductListEntityPage
    {
        public ProductListPage(ProductListViewModel viewModel) : base(viewModel)
        {
            //var filter = new UomListFilter();
            //filter.NameSearchTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(ViewModel.NameSearchText)) { Source = ViewModel, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            //ColletionFilters = filter;
            InitializeDefaulPage();
        }
    }
}
