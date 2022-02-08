using MyStock.Application.Vendors.Pages;

namespace MyStock.Application.Vendors
{
    public class VendorListViewModel : EntityListPageViewModel<Vendor, IVendorListEntityPage>, IVendorListViewModel
    {
        private string nameSearchText = "";

        public VendorListViewModel(IContext context) : base(context)
        {
            Title = Translations.Vendors;
        }

        public string NameSearch
        {
            get => nameSearchText;
            set => this.RaiseAndSetIfChanged(ref nameSearchText, value, nameof(NameSearch), nameof(Collection));
        }

        public override bool CanDeleteEntity(Vendor entity, out string reason)
        {
            reason = string.Empty;
            if (entity.Purchases.Any())
            {
                reason = Translations.VendorCannotBeDeleted;
                return false;
            }
            return true;
        }

        protected override ICollection<ColumnViewModel> BuildColums()
        {
            return new List<ColumnViewModel>()
            {
                new (Translations.LastName, nameof(Vendor.LastName),1),
                new (Translations.FirstName, nameof(Vendor.FirstName),2),
                new (Translations.MiddleName, nameof(Vendor.MiddleName),3),
                new (Translations.Phone, nameof(Vendor.Phone),4),
                new (Translations.Address, nameof(Vendor.Address),5)
            };
        }

        protected override IViewable CreateEntityViewModel(Vendor entity) => new VendorViewModel(entity, Context);

        protected override bool FilereItem(Vendor entity)
        {
            if (string.IsNullOrWhiteSpace(nameSearchText)) return true;

            return entity.FirstName.Contains(nameSearchText) ||
                entity.LastName.Contains(nameSearchText) ||
                entity.MiddleName.Contains(nameSearchText);
        }

        protected override Vendor CreateNewEntity() =>
            new Vendor();
    }

}
