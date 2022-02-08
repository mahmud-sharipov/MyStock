namespace MyStock.Application.Customers
{
    public interface ICustomerListViewModel : IEntityListPageViewModel<Customer>
    {
        public string NameSearch { get; set; }
    }
}
