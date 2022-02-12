using MyStock.Application.Customers;
using MyStock.Application.Documents;
using MyStock.Application.Sale.Pages;
using MyStock.Application.Sale.Validators;

namespace MyStock.Application.Sale
{
    public class SalesViewModel : EntityPageViewModel<Sales, SalesValidator, ISalesEntityPage>
    {
        private DateTime date;
        private Customer customer;
        private string description;
        private decimal discount;

        public SalesViewModel(Sales entity, IContext context) : base(entity, context)
        {
            Date = DateTime.Now.Date;
        }

        public DateTime Date { get => date; set => RaiseAndSetAndValidateIfChanged(ref date, value); }

        public Customer Customer { get => customer; set => RaiseAndSetAndValidateIfChanged(ref customer, value); }

        public string Description { get => description; set => RaiseAndSetAndValidateIfChanged(ref description, value); }

        public decimal Discount
        {
            get => discount;
            set
            {
                if (RaiseAndSetAndValidateIfChanged(ref discount, value))
                    RaisePropertyChanged(new[] { nameof(Balance) });
            }
        }

        public decimal TotalPrice => Details.Sum(d => d.TotalPrice);

        public decimal Balance => TotalPrice - Math.Min(Discount, TotalPrice);

        public ObservableCollection<DocumentDetailViewModel> Details { get; set; }

        public CustomerSearchViewModel CustomerSearchViewModel => new CustomerSearchViewModel(Context, c => Customer = c);

        protected override void InitializeAssociatedProperties()
        {
            base.InitializeAssociatedProperties();
        }

        public override bool ValidateBeforeSaveChange()
        {
            return base.ValidateBeforeSaveChange();
        }

        public override void ActionBeforeSave()
        {
            base.ActionBeforeSave();
        }
    }
}