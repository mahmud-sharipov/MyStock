using MyStock.Application.Purchases;
using MyStock.Application.Purchases.Pages;

namespace MyStock.Pages.Purchases
{
    public partial class PurchasePage : IPurchaseEntityPage
    {
        public PurchasePage(PurchaseViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
