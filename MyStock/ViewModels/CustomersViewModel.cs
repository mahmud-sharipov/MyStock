namespace MyStock.ViewModels;

public class CustomersViewModel : BaseViewModel
{
    public CustomersViewModel() : base() { }

    private Customer _selectedCustomer;

    public Customer SelectedCustomer
    {
        get => _selectedCustomer;
        set => SetProperty(ref _selectedCustomer, value);
    }

    ObservableCollection<Customer> _customers;
    public ObservableCollection<Customer> Customers
    {
        get
        {
            if (_customers == null)
                _customers = new ObservableCollection<Customer>(Context.GetAll<Customer>());

            return _customers;
        }
    }

    #region Commands

    public RelayCommand AddNewCustomer => new RelayCommand(async param =>
    {
        Customer newCustomer = new();
        if(await DialogManger.Show<AddNewCustomerDialog, Customer>(newCustomer))
        {
            Context.Add(newCustomer);
            Customers.Add(newCustomer);
            Context.SaveChanges();
        }
    });
    #endregion

    protected override void OnDispose()
    {
        _customers = null;
        base.OnDispose();
    }
}