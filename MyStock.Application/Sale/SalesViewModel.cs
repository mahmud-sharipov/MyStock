using MyStock.Application.Customers;
using MyStock.Application.Documents;
using MyStock.Application.Sale.Pages;
using MyStock.Application.Sale.Validators;
using MyStock.Core.Report;

namespace MyStock.Application.Sale
{
    public class SalesViewModel : DocumentViewModel<Sales, SalesValidator, ISalesEntityPage>
    {
        private Customer customer;

        public SalesViewModel(Sales entity, IContext context) : base(entity, context) { }

        public Customer Customer { get => customer; set => RaiseAndSetAndValidateIfChanged(ref customer, value); }

        public CustomerSearchViewModel CustomerSearchViewModel => new CustomerSearchViewModel(Context, c => Customer = c)
        {
            SelectedSearchItem = Entity.Customer,
            SearchText = Entity.Customer?.Title ?? ""
        };

        protected override void BuildCommands()
        {
            base.BuildCommands();
            Report = ReactiveCommand.Create(() =>
            {
                ReportBuilder.New(@"Assets\Reports\SalesInvoice.html")
                    .UseDefaultStyles()
                    .AddParameter(ReportGlobalConsts.ConpanyName, Global.Settings.CompanyName)
                    .AddParameter(SalesInvoiceReportParameters.CustomerName, Customer?.FullName ?? "")
                    .AddParameter(SalesInvoiceReportParameters.CustomerAddress, string.IsNullOrEmpty(Customer?.Address) ? "__________________" : Customer.Address)
                    .AddParameter(SalesInvoiceReportParameters.CustomerPhone, string.IsNullOrEmpty(Customer?.Phone) ? "__________________" : Customer.Phone)
                    .AddParameter(SalesInvoiceReportParameters.Number, Entity.Number.ToString())
                    .AddParameter(SalesInvoiceReportParameters.Date, Date.ToString(Global.DateFormate))
                    .AddParameter(SalesInvoiceReportParameters.Details, string.Join("", Details.Select(GetDetailInfoForReport)))
                    .AddParameter(SalesInvoiceReportParameters.Subtotal, Subtotal.ToString("C2"))
                    .AddParameter(SalesInvoiceReportParameters.Discount, Discount.ToString("C2"))
                    .AddParameter(SalesInvoiceReportParameters.Total, Total.ToString("C2"))
                    .AddParameter(SalesInvoiceReportParameters.Paid, PaidAmount.ToString("C2"))
                    .AddParameter(SalesInvoiceReportParameters.Balance, Balance.ToString("C2"))
                    .GenerateAndOpen(@"D:/Test.pdf");

            }, outputScheduler: Scheduler.CurrentThread);
        }

        protected override void OnProcess()
        {
            foreach (var detail in Details)
            {
                var stockLevel = detail.Product.StockLevels.Single(sl => sl.WarehouseGuid == detail.Warehouse.Guid);
                stockLevel.NetQuantity -= detail.Quantity;
                detail.RaisePropertyChanged(nameof(detail.Product));
                Entity.IsFullyPaid = Balance == 0;
            }
        }

        protected override void OnUnprocess()
        {
            foreach (var detail in Details)
            {
                var stockLevel = detail.Product.StockLevels.Single(sl => sl.WarehouseGuid == detail.Warehouse.Guid);
                stockLevel.NetQuantity += detail.Quantity;
                detail.RaisePropertyChanged(nameof(detail.Product));
                Entity.IsFullyPaid = Balance == 0;
            }
        }
    }

    public static class SalesInvoiceReportParameters
    {
        public const string CustomerName = "$CustomerName$";
        public const string CustomerAddress = "$CustomerAddress$";
        public const string CustomerPhone = "$CustomerPhone$";
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