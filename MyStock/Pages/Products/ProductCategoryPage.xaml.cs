using MyStock.Application.Category;

namespace MyStock.Pages.Products
{
    public partial class ProductCategoryPage : IProductCategoryEntityPage
    {
        public ProductCategoryPage(ProductCategoryViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
