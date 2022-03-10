using MyStock.Application.Customers.Pages;

namespace MyStock.Application.Customers
{
    public class CustomerListViewModel : EntityListPageViewModel<Customer, ICustomerListEntityPage>, ICustomerListViewModel
    {
        private string nameSearchText;

        public CustomerListViewModel(IContext context) : base(context)
        {
            Title = Translations.Customers;
        }

        public string NameSearch
        {
            get => nameSearchText;
            set => this.RaiseAndSetIfChanged(ref nameSearchText, value, nameof(NameSearch), nameof(Collection));
        }

        public override bool CanDeleteEntity(Customer entity, out string reason)
        {
            reason = string.Empty;
            if (entity.IsAnonymous)
            {
                reason = Translations.DefaultAccountCannotBedeleted;
                return false;
            }

            if (entity.Sales.Any())
            {
                reason = Translations.CustomerCannotBeDeletedMessage;
                return false;
            }

            return true;
        }

        protected override ICollection<ColumnViewModel> BuildColums()
        {
            return new List<ColumnViewModel>()
            {
                new (Translations.LastName, nameof(Customer.LastName),1),
                new (Translations.FirstName, nameof(Customer.FirstName),2),
                new (Translations.MiddleName, nameof(Customer.MiddleName),3),
                new (Translations.Phone, nameof(Customer.Phone),4),
                new (Translations.Address, nameof(Customer.Address),5)
            };
        }

        protected override IViewable CreateEntityViewModel(Customer entity) => new CustomerViewModel(entity, Context);

        protected override Expression<Func<Customer, bool>> FilereItem()
        {
            if (string.IsNullOrWhiteSpace(nameSearchText)) return e => true;

            return entity => entity.FirstName.Contains(nameSearchText) ||
                entity.LastName.Contains(nameSearchText) ||
                entity.MiddleName.Contains(nameSearchText);
        }

        protected override Customer CreateNewEntity()
        {
            return new Customer();
        }
    }
}