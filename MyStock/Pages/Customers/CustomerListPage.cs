using MyStock.Application.Customers;
using MyStock.Application.Customers.Pages;

namespace MyStock.Pages.Customers
{
    public class CustomerListPage : EntityListPage<CustomerListViewModel>, ICustomerListEntityPage
    {
        public CustomerListPage(CustomerListViewModel viewModel) : base(viewModel)
        {
            //var filter = new UomListFilter();
            //filter.NameSearchTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(ViewModel.NameSearchText)) { Source = ViewModel, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            //ColletionFilters = filter;
            InitializeDefaulPage();
        }
    }
}
