using MyStock.Application.Products;
using MyStock.Application.Products.Pages;

namespace MyStock.Pages.Products
{
    public partial class ProductPage : IProductEntityPage
    {
        public ProductPage(ProductViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
