using MyStock.Application.Documents;
using MyStock.Application.Purchases.Pages;
using MyStock.Application.Purchases.Validators;
using MyStock.Application.Vendors;
using MyStock.Core.Report;

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
                Entity.IsFullyPaid = Balance == 0;
            }
        }

        protected override void OnUnprocess()
        {
            foreach (var detail in Details)
            {
                var stockLevel = detail.Product.StockLevels.Single(sl => sl.WarehouseGuid == detail.Warehouse.Guid);
                stockLevel.NetQuantity -= detail.Quantity;
                detail.RaisePropertyChanged(nameof(detail.Product));
                Entity.IsFullyPaid = Balance == 0;
            }
        }

        protected override void BuildCommands()
        {
            base.BuildCommands();
            Report = ReactiveCommand.Create(() =>
            {
                ReportBuilder.New(@"Assets\Reports\PurchaseInvoice.html")
                    .UseDefaultStyles()
                    .AddParameter(ReportGlobalConsts.ConpanyName, Global.Settings.CompanyName)
                    .AddParameter(PurchaseInvoiceReportParameters.VendorName, Vendor?.FullName ?? "")
                    .AddParameter(PurchaseInvoiceReportParameters.VendorAddress, string.IsNullOrEmpty(Vendor?.Address) ? "__________________" : Vendor.Address)
                    .AddParameter(PurchaseInvoiceReportParameters.VendorPhone, string.IsNullOrEmpty(Vendor?.Phone) ? "__________________" : Vendor.Phone)
                    .AddParameter(PurchaseInvoiceReportParameters.Number, Entity.Number.ToString())
                    .AddParameter(PurchaseInvoiceReportParameters.Date, Date.ToString(Global.DateFormate))
                    .AddParameter(PurchaseInvoiceReportParameters.Details, string.Join("", Details.Select(GetDetailInfoForReport)))
                    .AddParameter(PurchaseInvoiceReportParameters.Total, Total.ToString("C2"))
                    .AddParameter(PurchaseInvoiceReportParameters.Paid, PaidAmount.ToString("C2"))
                    .AddParameter(PurchaseInvoiceReportParameters.Balance, Balance.ToString("C2"))
                    .GenerateAndOpen(@"D:/Test.pdf");

            }, outputScheduler: Scheduler.CurrentThread);
        }
    }

    public static class PurchaseInvoiceReportParameters
    {
        public const string VendorName = "$VendorName$";
        public const string VendorAddress = "$VendorAddress$";
        public const string VendorPhone = "$VendorPhone$";
        public const string Number = "$Number$";
        public const string Date = "$Date$";
        public const string Subtotal = "$Subtotal$";
        public const string Discount = "$Discount$";
        public const string Total = "$Total$";
        public const string Paid = "$Paid$";
        public const string Balance = "$Balance$";
        public const string Details = "$Details$";
    }
}