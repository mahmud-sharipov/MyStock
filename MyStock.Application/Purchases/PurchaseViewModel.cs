using MyStock.Application.Documents;
using MyStock.Application.Purchases.Pages;
using MyStock.Application.Purchases.Validators;
using MyStock.Application.Vendors;

namespace MyStock.Application.Purchases
{
    public class PurchaseViewModel : DocumentViewModel<Purchase, PurchaseValidator, IPurchaseEntityPage>, IPurchaseViewModel
    {
        private Vendor _vendor;

        public PurchaseViewModel(Purchase entity, IContext context) : base(entity, context)
        {
        }

        public Vendor Vendor { get => _vendor; set => RaiseAndSetAndValidateIfChanged(ref _vendor, value); }
        public VendorSearchViewModel VendorSearchViewModel => new VendorSearchViewModel(Context, c => Vendor = c)
        {
            SelectedSearchItem = Entity.Vendor,
            SearchText = Entity.Vendor?.Title ?? ""
        };

        protected override void OnProcess()
        {
            foreach (var detail in Details)
            {
                var stockLevel = detail.Product.StockLevels.Single(sl => sl.WarehouseGuid == detail.Warehouse.Guid);
                stockLevel.NetQuantity += detail.Quantity;
                detail.RaisePropertyChanged(nameof(detail.Product));
            }
        }

        protected override void OnUnprocess()
        {
            foreach (var detail in Details)
            {
                var stockLevel = detail.Product.StockLevels.Single(sl => sl.WarehouseGuid == detail.Warehouse.Guid);
                stockLevel.NetQuantity -= detail.Quantity;
                detail.RaisePropertyChanged(nameof(detail.Product));
            }
        }
    }
}