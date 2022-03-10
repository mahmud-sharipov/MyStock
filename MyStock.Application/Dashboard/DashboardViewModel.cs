using MyStock.Core.Report;

namespace MyStock.Application.Dashboard;
public class DashboardViewModel : ViewModel, IDashboardViewModel
{
    public DashboardViewModel(IContext context) : base(context)
    {
        InitData();
        BuldCommand();
    }

    private void InitData()
    {
        var query = Context.Set<ProductStockLevel>().Where(s => s.MaxQuantity > 0);
        ProductsCountInfo = new ProductsCountInfo()
        {
            OutOfStockItemsCount = query.Where(s => s.NetQuantity <= 0).Count(),
            LowStockItemsCount = query.Where(s => s.NetQuantity > 0 && s.NetQuantity < s.MinQuantity).Count(),
            OverStockItemsCount = query.Where(s => s.NetQuantity > s.MaxQuantity).Count(),
            AllProductsCount = Context.Set<Product>().Count(),
            InactiveProductsCount = Context.Set<Product>().Where(p => !p.IsActive).Count(),
        };

        TopSellingProducts = Context.Set<DocumentDetail>()
            .Where(d => d.Document.Date > DateTime.Now.AddDays(-30)).ToList()
            .GroupBy(d => d.Product)
            .Select(d => new TopSellingProduct()
            {
                Category = d.Key.Category.Name,
                Code = d.Key.Code,
                Description = d.Key.Description,
                Uom = d.Key.Uom.Code,
                Quantity = d.Sum(d => d.Quantity)
            }).OrderByDescending(d => d.Quantity).Take(10).ToList();

        CustomerCredits = Context.Set<Sales>().Where(s => !s.IsFullyPaid).ToList()
            .GroupBy(d => d.Customer)
            .Select(d => new CustomerCredit()
            {
                Customer = d.Key,
                Credit = d.Sum(d => d.Balance)
            }).ToList();

        var lastTwoManth = DateTime.Now.AddDays(-60);
        Expenses = Context.Set<DocumentDetail>().Where(d => d.Document.Date > lastTwoManth && d.Document is Purchase)
            .GroupBy(d => d.Document.Date)
            .Select(d => new MonthlySummary() { Date = d.Key, Amount = d.Sum(dd => dd.Quantity * dd.UnitPrice) }).ToList();

        Sales = Context.Set<DocumentDetail>().Where(d => d.Document.Date > lastTwoManth && d.Document is Sales)
            .GroupBy(d => d.Document.Date)
            .Select(d => new MonthlySummary() { Date = d.Key, Amount = d.Sum(dd => dd.Quantity * dd.UnitPrice) }).ToList();
    }

    private void BuldCommand()
    {
        ReportCustomersWithCredit = ReactiveCommand.Create(() =>
        {
            ReportBuilder.New(@"Assets\Reports\CustomersWithCredit.html")
                .UseDefaultStyles()
                .AddParameter(ReportGlobalConsts.ConpanyName, Global.Settings.CompanyName)
                .AddParameter("$Customers$", string.Join("", CustomerCredits.Select(GetCustomerInfoForReport)))
                .GenerateAndOpen(@"D:/Test.pdf");

        }, outputScheduler: Scheduler.CurrentThread);

        ReportProfitAndLost = ReactiveCommand.Create(() =>
        {
            var summary = Context.Set<Document>()
            .Select(d => new
            {
                TotalPrice = d.Details.Sum(dd => dd.Quantity * dd.UnitPrice),
                Discount = d.Discount,
                d.Date.Year,
                d.Date.Month,
                IsIncome = d is Sales
            }).ToList()
            .GroupBy(d => new { d.Month, d.Year, d.IsIncome })
            .Select(d => new
            {
                TotalPrice = d.Sum(_ => _.TotalPrice),
                Discount = d.Sum(d => d.Discount),
                Date = new DateTime(d.Key.Year, d.Key.Month, 1),
                d.Key.IsIncome
            });

            var res = "";
            decimal beginingBalance = 0;
            foreach (var item in summary.GroupBy(d => d.Date).OrderBy(d => d.Key))
            {
                var incomeItem = item.FirstOrDefault(item => item.IsIncome);
                var expensesItem = item.FirstOrDefault(item => !item.IsIncome);
                var income = (incomeItem?.TotalPrice ?? 0) - (incomeItem?.Discount ?? 0);
                var expenses = (expensesItem?.TotalPrice ?? 0) - (expensesItem?.Discount ?? 0);
                var profit = income - expenses;
                beginingBalance += profit;
                res += $"<tr><td>{item.Key:MMM yyyy}</td><td>{income:C2}</td><td>{expenses:C2}</td><td>{profit:C2}</td><td>{beginingBalance:C2}</td></tr>";
            }

            ReportBuilder.New(@"Assets\Reports\ProfitAndLost.html")
                .UseDefaultStyles()
                .AddParameter(ReportGlobalConsts.ConpanyName, Global.Settings.CompanyName)
                .AddParameter("$P&L$", res)
                .GenerateAndOpen(@"D:/Test.pdf");

        }, outputScheduler: Scheduler.CurrentThread);
    }

    protected string GetCustomerInfoForReport(CustomerCredit detailViewModel, int index)
    {
        return $"<tr><td>{++index}</td><td>{detailViewModel.Customer.FullName}</td><td>{detailViewModel.Credit:C2}</td><td>{detailViewModel.Customer.Phone}</td><td>{detailViewModel.Customer.Address}</td></tr>";
    }

    public ICommand ReportCustomersWithCredit { get; set; }
    public ICommand ReportProfitAndLost { get; set; }
    public ProductsCountInfo ProductsCountInfo { get; set; }
    public List<TopSellingProduct> TopSellingProducts { get; set; }
    public List<CustomerCredit> CustomerCredits { get; set; }
    public List<MonthlySummary> Expenses { get; set; }
    public List<MonthlySummary> Sales { get; set; }

    IDashboardPage _entityPage;
    public IDashboardPage EntityPage =>
        _entityPage ??= Global.Container.Resolve<IDashboardPage>(new PositionalParameter(0, this));

    IEntityListPage INavigatable.EntityPage => EntityPage;
}

public class ProductsCountInfo
{
    public int OutOfStockItemsCount { get; set; }
    public int LowStockItemsCount { get; set; }
    public int OverStockItemsCount { get; set; }
    public int AllProductsCount { get; set; }
    public int InactiveProductsCount { get; set; }
}

public class TopSellingProduct
{
    public string Code { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Uom { get; set; }
    public decimal Quantity { get; set; }
}

public class CustomerCredit
{
    public Customer Customer { get; set; }
    public decimal Credit { get; set; }
}

public class MonthlySummary
{
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
}

//public class MonthlyProfitAdLostSummary
//{
//    public DateTime Date { get; set; }
//    public decimal Expenses { get; set; }
//    public decimal Income { get; set; }
//    public decimal Profit { get; set; }
//    public decimal Balance { get; set; }
//}