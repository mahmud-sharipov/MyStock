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

        public SalesViewModel(Sales entity, IContext context) : base(entity, context)
        {

        }

        public Customer Customer { get => customer; set => RaiseAndSetAndValidateIfChanged(ref customer, value); }

        public CustomerSearchViewModel CustomerSearchViewModel => new CustomerSearchViewModel(Context, c => Customer = c)
        {
            SelectedSearchItem = Entity.Customer,
            SearchText = Entity.Customer?.Title ?? ""
        };


        public ICommand Report { get; set; }

        protected override void BuildCommands()
        {
            base.BuildCommands();
            Report = ReactiveCommand.Create(() =>
            {
                ReportBuilder.New(@"Assets\Reports\SalesInvoice.html")
                    .UseDefaultStyles()
                    .AddParameter(SalesInvoiceReportParameters.ConpanyName, "MyStore")
                    .AddParameter(SalesInvoiceReportParameters.CustomerName, Customer.FullName)
                    .AddParameter(SalesInvoiceReportParameters.CustomerAddress, Customer.Address)
                    .AddParameter(SalesInvoiceReportParameters.CustomerPhone, Customer.Phone)
                    .AddParameter(SalesInvoiceReportParameters.Number, "____")
                    .AddParameter(SalesInvoiceReportParameters.Date, Date.ToString("D"))
                    .AddParameter(SalesInvoiceReportParameters.Details, string.Join("", Details.Select(GetDetailInfoForReport)))
                    .AddParameter(SalesInvoiceReportParameters.Subtotal, Subtotal.ToString("C2"))
                    .AddParameter(SalesInvoiceReportParameters.Discount, Discount.ToString("C2"))
                    .AddParameter(SalesInvoiceReportParameters.Total, Total.ToString("C2"))
                    .AddParameter(SalesInvoiceReportParameters.Paid, PaidAmount.ToString("C2"))
                    .AddParameter(SalesInvoiceReportParameters.Balance, Balance.ToString("C2"))
                    .GenerateAndOpen(@"D:/Test.pdf");

            }, outputScheduler: Scheduler.CurrentThread);
        }

        string GetDetailInfoForReport(DocumentDetailViewModel detailViewModel, int index)
        {
            return $"<tr><td>{index++}</td><td>{detailViewModel.Product.Code}</td><td>{detailViewModel.Product.Description}</td><td>{detailViewModel.Product.Category.Name}</td><td>{detailViewModel.Quantity:N2}</td><td>{detailViewModel.UnitPrice:C2}</td><td>{detailViewModel.TotalPrice:C2}</td></tr>";
        }

        protected override void OnProcess()
        {
            foreach (var detail in Details)
            {
                var stockLevel = detail.Product.StockLevels.Single(sl => sl.WarehouseGuid == detail.Warehouse.Guid);
                stockLevel.NetQuantity -= detail.Quantity;
                detail.RaisePropertyChanged(nameof(detail.Product));
            }
        }

        protected override void OnUnprocess()
        {
            foreach (var detail in Details)
            {
                var stockLevel = detail.Product.StockLevels.Single(sl => sl.WarehouseGuid == detail.Warehouse.Guid);
                stockLevel.NetQuantity += detail.Quantity;
                detail.RaisePropertyChanged(nameof(detail.Product));
            }
        }
    }

    public static class SalesInvoiceReportParameters
    {
        public const string ConpanyName = "$CompanyName$";
        public const string CustomerName = "$CustomerName$";
        public const string CustomerAddress = "$CustomerAddress$";
        public const string CustomerPhone = "$CompanyPhone$";
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