using MyStock.Application.Vendors;
using MyStock.Application.Vendors.Pages;

namespace MyStock.Pages.Vendors
{
    public partial class VendorPage : IVendorEntityPage
    {
        public VendorPage(VendorViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
