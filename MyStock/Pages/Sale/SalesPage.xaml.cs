using MyStock.Application.Sale;
using MyStock.Application.Sale.Pages;

namespace MyStock.Pages.Sale
{
    public partial class SalesPage : ISalesEntityPage
    {
        public SalesPage(SalesViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
