using MyStock.Application.Uoms.Pages;

namespace MyStock.Application.Uoms
{
    public class UomListViewModel : EntityListPageViewModel<Uom, IUomListEntityPage>, IUomListViewModel
    {
        private string nameSearchText;

        public UomListViewModel(IContext context) : base(context)
        {
            Title = Translations.UOMs;
        }

        public string NameSearchText
        {
            get => nameSearchText;
            set => this.RaiseAndSetIfChanged(ref nameSearchText, value, nameof(NameSearchText), nameof(Collection));
        }

        public override bool CanDeleteEntity(Uom entity, out string reason)
        {
            reason = "";
            if (entity.Products.Any() || entity.DocumentDetails.Any())
            {
                reason = Translations.UomUsedInDocumentsAndProductsMessage;
                return false;
            }
            return true;
        }

        protected override ICollection<ColumnViewModel> BuildColums()
        {
            return new List<ColumnViewModel>()
            {
                new ColumnViewModel(Translations.Name, nameof(UomViewModel.Name),0),
                new ColumnViewModel(Translations.Code, nameof(UomViewModel.Code),1),
                new ColumnViewModel(Translations.Description, nameof(UomViewModel.Description),2),
            };
        }

        protected override IViewable CreateEntityViewModel(Uom entity) => new UomViewModel(entity, Context);

        protected override Expression<Func<Uom, bool>> FilereItem()
        {
            if (string.IsNullOrEmpty(NameSearchText))
                return e => true;
            return e => e.Name.Contains(NameSearchText, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
