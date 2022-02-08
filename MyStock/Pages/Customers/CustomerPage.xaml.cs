using MyStock.Application.Customers;
using MyStock.Application.Customers.Pages;

namespace MyStock.Pages.Customers
{
    public partial class CustomerPage : ICustomerEntityPage
    {
        public CustomerPage(CustomerViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
        }
    }
}
