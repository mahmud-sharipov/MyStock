using MyStock.Application.Sale.Pages;

namespace MyStock.Application.Sale
{
    public class SalesListViewModel : EntityListPageViewModel<Sales, ISalesListEntityPage>, ISalesListViewModel
    {
        public SalesListViewModel(IContext context) : base(context)
        {
            Title =Translations.Sales;
        }

        public override bool CanDeleteEntity(Sales entity, out string reason)
        {
            reason = string.Empty;
            return true;
        }

        protected override ICollection<ColumnViewModel> BuildColums()
        {
            return new List<ColumnViewModel>()
            {
                new ColumnViewModel(Translations.Date, nameof(SalesViewModel.Date),0),
                new ColumnViewModel(Translations.Customer, $"{nameof(SalesViewModel.Customer)}.{nameof(Customer.FullName)}",1),
                new ColumnViewModel(Translations.Description, nameof(SalesViewModel.Description),2),
                new ColumnViewModel(Translations.Discount, nameof(SalesViewModel.Discount),3),
                new ColumnViewModel(Translations.TotalPrice, nameof(SalesViewModel.TotalPrice),4),
                new ColumnViewModel(Translations.Balance, nameof(SalesViewModel.Balance),5),
            };
        }

        protected override IViewable CreateEntityViewModel(Sales entity) => new SalesViewModel(entity, Context);

        protected override bool FilereItem(Sales entity)
        {
            return true;
        }

        protected override Sales CreateNewEntity()
        {
            var entity = new Sales();
            return entity;
        }
    }

}
