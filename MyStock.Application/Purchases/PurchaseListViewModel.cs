using MyStock.Application.Documents;
using MyStock.Application.Purchases.Pages;
using MyStock.Core.Report;

namespace MyStock.Application.Purchases
{
    public class PurchaseListViewModel : EntityListPageViewModel<Purchase, IPurchaseListEntityPage>, IPurchaseListViewModel
    {
        private DateTime? _fromDate;
        private DateTime? _toDate;
        private DocumentStatus _status;
        private Report _selectedReport;

        public PurchaseListViewModel(IContext context) : base(context)
        {
            Title = Translations.Purchases;
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
                new Report() { Name = "Счет-Фактура Покупок", FileName = "PurchaseInvoices.html", Action = PrintInvoicesReport },
                new Report() { Name = "Покупки по Товарам", FileName = "PurchaseInvoicesDetailed.html", Action = PrintDetailedInvoicesReport },
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
                .AddParameter(PurchaseInvoicesReportParameters.Invoices, string.Join("", _collection.Select(GetinvoiceInfoForReport)))
                .AddParameter(PurchaseInvoicesReportParameters.Total, (subtotal - discount).ToString("C2"))
                .AddParameter(PurchaseInvoicesReportParameters.Paid, paid.ToString("C2"))
                .AddParameter(PurchaseInvoicesReportParameters.Balance, (subtotal - discount - paid).ToString("C2"))
                .GenerateAndOpen(@"D:/Test.pdf");
        }

        protected string GetinvoiceInfoForReport(Purchase purchase, int index)
        {
            return $@"<tr><td>{purchase.Number}</td><td>{purchase.Date.ToString("dd.MM.yyyy")}</td><td>{purchase.Vendor.FullName}</td><td>{purchase.Total:C2}</td><td>{purchase.PaidAmount:C2}</td><td>{purchase.Balance:C2}</td></tr>";
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
                .AddParameter(PurchaseInvoicesReportParameters.Invoices, list)
                .AddParameter(PurchaseInvoicesReportParameters.Subtotal, subtotal.ToString("C2"))
                .AddParameter(PurchaseInvoicesReportParameters.Discount, discount.ToString("C2"))
                .AddParameter(PurchaseInvoicesReportParameters.Total, (subtotal - discount).ToString("C2"))
                .GenerateAndOpen(@"D:/Test.pdf");
        }

        public override bool CanDeleteEntity(Purchase entity, out string reason)
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
                new ColumnViewModel(Translations.Date, nameof(PurchaseViewModel.Date),0,Global.DateFormate),
                new ColumnViewModel(Translations.DocNumber, nameof(Document.Number),1),
                new ColumnViewModel(Translations.Vendor, $"{nameof(PurchaseViewModel.Vendor)}.{nameof(Customer.FullName)}",2),
                new ColumnViewModel(Translations.DocumentTotal, nameof(PurchaseViewModel.Total),6,"C2"),
                new ColumnViewModel(Translations.PaidAmount, nameof(PurchaseViewModel.PaidAmount),7,"C2"),
                new ColumnViewModel(Translations.Balance, nameof(PurchaseViewModel.Balance),8,"C2"),
            };
        }

        protected override IViewable CreateEntityViewModel(Purchase entity) => new PurchaseViewModel(entity, Context);

        protected override Purchase CreateNewEntity()
        {
            var entity = new Purchase()
            {
                Vendor = Global.Settings.DefaultAnonymousVendorOnNewPurchase ? Context.Get<Vendor>(Vendor.AnonymousVendorGuid) : null,
                Number = Global.Settings.NextPurchaseDocNumber++
            };
            Global.Context.SaveChanges();
            return entity;
        }

        protected override Expression<Func<Purchase, bool>> FilereItem()
        {
            Expression<Func<Purchase, bool>> dateExpression = this switch
            {
                { FromDate: not null, ToDate: not null } => e => e.Date >= FromDate && e.Date <= ToDate,
                { FromDate: not null } => e => e.Date >= FromDate,
                { ToDate: not null } => e => e.Date <= ToDate,
                _ => e => true,
            };

            Expression<Func<Purchase, bool>> statusExpression = Status switch
            {
                DocumentStatus.Paid => e => e.IsFullyPaid,
                DocumentStatus.NotPaid => e => !e.IsFullyPaid,
                _ => e => true,
            };

            var param = Expression.Parameter(typeof(Purchase), "e");
            var body = Expression.AndAlso(
                Expression.Invoke(statusExpression, param),
                Expression.Invoke(dateExpression, param));

            return Expression.Lambda<Func<Purchase, bool>>(body, param);
        }

        protected override IOrderedQueryable<Purchase> Order(IQueryable<Purchase> source)
        {
            return source.OrderBy(e => e.Date);
        }
    }
    public static class PurchaseInvoicesReportParameters
    {
        public const string Subtotal = "$Subtotal$";
        public const string Discount = "$Discount$";
        public const string Total = "$Total$";
        public const string Paid = "$Paid$";
        public const string Balance = "$Balance$";
        public const string Invoices = "$Invoices$";
    }
}
