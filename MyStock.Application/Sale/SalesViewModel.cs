using MyStock.Application.Customers;
using MyStock.Application.Documents;
using MyStock.Application.Sale.Pages;
using MyStock.Application.Sale.Validators;

namespace MyStock.Application.Sale
{
    public class SalesViewModel : DocumentViewModel<Sales, SalesValidator, ISalesEntityPage>
    {
        private Customer customer;

        public SalesViewModel(Sales entity, IContext context) : base(entity, context)
        {

        }

        public Customer Customer { get => customer; set => RaiseAndSetAndValidateIfChanged(ref customer, value); }

        public CustomerSearchViewModel CustomerSearchViewModel => new CustomerSearchViewModel(Context, c => Customer = c) { SelectedSearchItem = Entity.Customer, SearchText = Entity.Customer.Title };
    }
}