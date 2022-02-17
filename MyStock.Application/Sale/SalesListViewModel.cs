using MyStock.Application.Sale.Pages;

namespace MyStock.Application.Sale
{
    public class SalesListViewModel : EntityListPageViewModel<Sales, ISalesListEntityPage>, ISalesListViewModel
    {
        public SalesListViewModel(IContext context) : base(context)
        {
            Title = Translations.Sales;
        }

        public override bool CanDeleteEntity(Sales entity, out string reason)
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
                new ColumnViewModel(Translations.Date, nameof(SalesViewModel.Date),0,"D"),
                new ColumnViewModel(Translations.Customer, $"{nameof(SalesViewModel.Customer)}.{nameof(Customer.FullName)}",1),
                new ColumnViewModel(Translations.Description, nameof(SalesViewModel.Description),2),
                new ColumnViewModel(Translations.DocumentSubtotal, nameof(SalesViewModel.Subtotal),3,"C2"),
                new ColumnViewModel(Translations.Discount, nameof(SalesViewModel.Discount),4,"C2"),
                new ColumnViewModel(Translations.DocumentTotal, nameof(SalesViewModel.Total),5,"C2"),
                new ColumnViewModel(Translations.PaidAmount, nameof(SalesViewModel.PaidAmount),6,"C2"),
                new ColumnViewModel(Translations.Balance, nameof(SalesViewModel.Balance),7,"C2"),
            };
        }

        protected override IViewable CreateEntityViewModel(Sales entity) => new SalesViewModel(entity, Context);

        protected override bool FilereItem(Sales entity)
        {
            return true;
        }

        protected override Sales CreateNewEntity()
        {
            var entity = new Sales() { Customer = Context.Get<Customer>(Customer.GeneralCustomerGuid) };
            return entity;
        }
    }

}
