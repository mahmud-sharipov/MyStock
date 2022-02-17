using MyStock.Application.Purchases.Pages;

namespace MyStock.Application.Purchases
{
    public class PurchaseListViewModel : EntityListPageViewModel<Purchase, IPurchaseListEntityPage>, IPurchaseListViewModel
    {
        public PurchaseListViewModel(IContext context) : base(context)
        {
            Title = Translations.Purchases;
        }

        public override bool CanDeleteEntity(Purchase entity, out string reason)
        {
            reason = "";
            if (entity.Processed)
                reason = Translations.DocumentCompletedAndCannotBeDeleted;
            return !entity.Processed;
        }

        protected override ICollection<ColumnViewModel> BuildColums()
        {
            return new List<ColumnViewModel>()
            {
                new ColumnViewModel(Translations.Date, nameof(PurchaseViewModel.Date),0,"D"),
                new ColumnViewModel(Translations.Customer, $"{nameof(PurchaseViewModel.Vendor)}.{nameof(Customer.FullName)}",1),
                new ColumnViewModel(Translations.Description, nameof(PurchaseViewModel.Description),2),
                new ColumnViewModel(Translations.DocumentSubtotal, nameof(PurchaseViewModel.Subtotal),3,"C2"),
                new ColumnViewModel(Translations.Discount, nameof(PurchaseViewModel.Discount),4,"C2"),
                new ColumnViewModel(Translations.DocumentTotal, nameof(PurchaseViewModel.Total),5,"C2"),
                new ColumnViewModel(Translations.PaidAmount, nameof(PurchaseViewModel.PaidAmount),6,"C2"),
                new ColumnViewModel(Translations.Balance, nameof(PurchaseViewModel.Balance),7,"C2"),
            };
        }

        protected override IViewable CreateEntityViewModel(Purchase entity) => new PurchaseViewModel(entity, Context);

        protected override bool FilereItem(Purchase entity)
        {
            return true;
        }

        protected override Purchase CreateNewEntity()
        {
            var entity = new Purchase();
            return entity;
        }
    }

}
