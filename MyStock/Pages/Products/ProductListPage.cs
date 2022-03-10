using MyStock.Application.Products;
using MyStock.Application.Products.Pages;

namespace MyStock.Pages.Products
{
    public class ProductListPage : EntityListPage<ProductListViewModel>, IProductListEntityPage
    {
        public ProductListPage(ProductListViewModel viewModel) : base(viewModel)
        {
            LeftContent = new ProductCategoryList() { DataContext = viewModel };
            HeaderContent = new ProductFilter() { DataContext = viewModel }; ;
            InitializeDefaulPage();
        }
    }
}
