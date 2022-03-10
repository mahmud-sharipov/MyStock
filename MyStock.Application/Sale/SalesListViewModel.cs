using MyStock.Application.Documents;
using MyStock.Application.Sale.Pages;
using MyStock.Core.Report;

namespace MyStock.Application.Sale
{
    public class SalesListViewModel : EntityListPageViewModel<Sales, ISalesListEntityPage>, ISalesListViewModel
    {
        private DateTime? _fromDate;
        private DateTime? _toDate;
        private DocumentStatus _status;
        private Report _selectedReport;

        public SalesListViewModel(IContext context) : base(context)
        {
            Title = Translations.Sales;
            FromDate = DateTime.Now.AddDays(-10);
            Status = DocumentStatus.All;
            Reports = InitReports();
            SelectedReport = Reports.FirstOrDefault();
        }

        public ICommand Report { get; set; }

        public DateTime? FromDate
        {
            get => _fromDate;
            set => this.RaiseAndSetIfChanged(ref _fromDate, value, nameof(FromDate), nameof(Collection));
        }

        public DateTime? ToDate
        {
            get => _toDate;
            set => this.RaiseAndSetIfChanged(ref _toDate, value, nameof(ToDate), nameof(Collection));
        }

        public DocumentStatus Status
        {
            get => _status;
            set => this.RaiseAndSetIfChanged(ref _status, value, nameof(Status), nameof(Collection));
        }

        public List<Report> Reports { get; }

        public Report SelectedReport
        {
            get => _selectedReport;
            set => this.RaiseAndSetIfChanged(ref _selectedReport, value);
        }

        List<Report> InitReports()
        {
            var reports = new List<Report>()
            {
                new Report() { Name = "Счет-Фактура Продаж", FileName = "SalesInvoices.html", Action = PrintInvoicesReport },
                new Report() { Name = "Продажи по Товарам", FileName = "SalesInvoicesDetailed.html", Action = PrintDetailedInvoicesReport },
            };
            return reports;
        }

        protected override void BuildCommands()
        {
            base.BuildCommands();
            Report = ReactiveCommand.Create(async () =>
            {
                if (SelectedReport != null)
                    await Task.Run(SelectedReport.Action);
            }, outputScheduler: Scheduler.CurrentThread);
        }

        void PrintInvoicesReport()
        {
            var query = Source.Where(FilereItem());
            var subtotal = query.SelectMany(_ => _.Details).Sum(_ => _.Quantity * _.UnitPrice);
            var discount = query.Sum(_ => _.Discount);
            var paid = query.Sum(_ => _.PaidAmount);

            ReportBuilder.New(@"Assets\Reports\" + SelectedReport.FileName)
                .UseDefaultStyles()
                .AddParameter(ReportGlobalConsts.ConpanyName, Global.Settings.CompanyName)
                .AddParameter(SalesInvoicesReportParameters.Invoices, string.Join("", _collection.Select(GetinvoiceInfoForReport)))
                .AddParameter(SalesInvoicesReportParameters.Subtotal, subtotal.ToString("C2"))
                .AddParameter(SalesInvoicesReportParameters.Discount, discount.ToString("C2"))
                .AddParameter(SalesInvoicesReportParameters.Total, (subtotal - discount).ToString("C2"))
                .AddParameter(SalesInvoicesReportParameters.Paid, paid.ToString("C2"))
                .AddParameter(SalesInvoicesReportParameters.Balance, (subtotal - discount - paid).ToString("C2"))
                .GenerateAndOpen(@"D:/Test.pdf");
        }

        protected string GetinvoiceInfoForReport(Sales sales, int index)
        {
            return $@"<tr><td>{sales.Number}</td><td>{sales.Date.ToString("dd.MM.yyyy")}</td><td>{sales.Customer.FullName}</td><td>{sales.Subtotal:C2}</td><td>{sales.Discount:C2}</td><td>{sales.Total:C2}</td><td>{sales.PaidAmount:C2}</td><td>{sales.Balance:C2}</td></tr>";
        }

        void PrintDetailedInvoicesReport()
        {
            var query = Source.Where(FilereItem());
            var subtotal = query.SelectMany(_ => _.Details).Sum(_ => _.Quantity * _.UnitPrice);
            var discount = query.Sum(_ => _.Discount);
            var details = query.SelectMany(_ => _.Details)
                .Select(_ => new
                {
                    _.Product.Code,
                    _.Product.Description,
                    Category = _.Product.Category.Name,
                    Uom = _.Product.Uom.Code,
                    _.Quantity,
                    _.UnitPrice,
                }).ToList()
                .GroupBy(_ => _.Code)
                .OrderBy(_ => _.Key);
            var list = "";
            int index = 0;
            foreach (var detail in details)
            {
                var item = detail.First();
                list += $@"<tr><td>{++index}</td><td>{item.Code}</td><td>{item.Description}</td><td>{item.Category}</td><td>{detail.Sum(_ => _.Quantity):N2} {item.Uom}</td><td>{detail.Sum(_ => _.Quantity * _.UnitPrice):C2}</td></tr>";
            }
            ReportBuilder.New(@"Assets\Reports\" + SelectedReport.FileName)
                .UseDefaultStyles()
                .AddParameter(ReportGlobalConsts.ConpanyName, Global.Settings.CompanyName)
                .AddParameter(SalesInvoicesReportParameters.Invoices, list)
                .AddParameter(SalesInvoicesReportParameters.Subtotal, subtotal.ToString("C2"))
                .AddParameter(SalesInvoicesReportParameters.Discount, discount.ToString("C2"))
                .AddParameter(SalesInvoicesReportParameters.Total, (subtotal - discount).ToString("C2"))
                .GenerateAndOpen(@"D:/Test.pdf");
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
                new ColumnViewModel(Translations.Date, nameof(SalesViewModel.Date),0,Global.DateFormate),
                new ColumnViewModel(Translations.DocNumber, nameof(Document.Number),1),
                new ColumnViewModel(Translations.Customer, $"{nameof(SalesViewModel.Customer)}.{nameof(Customer.FullName)}",2),
                new ColumnViewModel(Translations.DocumentSubtotal, nameof(SalesViewModel.Subtotal),4,"C2"),
                new ColumnViewModel(Translations.Discount, nameof(SalesViewModel.Discount),5,"C2"),
                new ColumnViewModel(Translations.DocumentTotal, nameof(SalesViewModel.Total),6,"C2"),
                new ColumnViewModel(Translations.PaidAmount, nameof(SalesViewModel.PaidAmount),7,"C2"),
                new ColumnViewModel(Translations.Balance, nameof(SalesViewModel.Balance),8,"C2"),
            };
        }

        protected override IViewable CreateEntityViewModel(Sales entity) => new SalesViewModel(entity, Context);

        protected override Sales CreateNewEntity()
        {
            var entity = new Sales()
            {
                Customer = Global.Settings.DefaultAnonymousCustomerOnNewSales ? Context.Get<Customer>(Customer.AnonymousCustomerGuid) : null,
                Number = Global.Settings.NextSalesDocNumber++
            };
            Global.Context.SaveChanges();
            return entity;
        }

        protected override Expression<Func<Sales, bool>> FilereItem()
        {
            Expression<Func<Sales, bool>> dateExpression = this switch
            {
                { FromDate: not null, ToDate: not null } => e => e.Date >= FromDate && e.Date <= ToDate,
                { FromDate: not null } => e => e.Date >= FromDate,
                { ToDate: not null } => e => e.Date <= ToDate,
                _ => e => true,
            };

            Expression<Func<Sales, bool>> statusExpression = Status switch
            {
                DocumentStatus.Paid => e => e.IsFullyPaid,
                DocumentStatus.NotPaid => e => !e.IsFullyPaid,
                _ => e => true,
            };

            var param = Expression.Parameter(typeof(Sales), "e");
            var body = Expression.AndAlso(
                Expression.Invoke(statusExpression, param),
                Expression.Invoke(dateExpression, param));

            return Expression.Lambda<Func<Sales, bool>>(body, param);
        }

        protected override IOrderedQueryable<Sales> Order(IQueryable<Sales> source)
        {
            return source.OrderBy(e => e.Date);
        }
    }

    public static class SalesInvoicesReportParameters
    {
        public const string Subtotal = "$Subtotal$";
        public const string Discount = "$Discount$";
        public const string Total = "$Total$";
        public const string Paid = "$Paid$";
        public const string Balance = "$Balance$";
        public const string Invoices = "$Invoices$";
    }
}
