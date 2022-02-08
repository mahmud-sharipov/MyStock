using MyStock.Application.Sale.Pages;
using MyStock.Application.Sale.Validators;

namespace MyStock.Application.Sale
{
    public class SalesViewModel : EntityPageViewModel<Sales, SalesValidator, ISalesEntityPage>
    {
        public SalesViewModel(Sales entity, IContext context) : base(entity, context)
        {
        }

        public DateTime Date { get; set; }
        public Customer Customer { get; set; }
        public string Description { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalPrice { get; }
        public decimal Balance { get; }

        public ObservableCollection<DocumentDetail> Details { get; set; }

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