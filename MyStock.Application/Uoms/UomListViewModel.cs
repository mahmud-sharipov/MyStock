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
                new ColumnViewModel(Translations.Name, nameof(IUomViewModel.Name),1),
                new ColumnViewModel(Translations.Code, nameof(IUomViewModel.Code),2),
                new ColumnViewModel(Translations.Description, nameof(IUomViewModel.Description),3),
            };
        }

        protected override IViewable CreateEntityViewModel(Uom entity) => new UomViewModel(entity, Context);

        protected override bool FilereItem(Uom entity)
        {
            return string.IsNullOrEmpty(NameSearchText) || entity.Name.Contains(NameSearchText, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
