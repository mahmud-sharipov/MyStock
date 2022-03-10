using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MyStock.Application.Assets.Lang;
using MyStock.Application.Dashboard;
using MyStock.Core.Interfaces;
using System.Linq;
using System.Windows.Media;

namespace MyStock.Pages.Dashboard
{
    public partial class DashboardPage : IDashboardPage
    {
        INavigatable IEntityListPage.ViewModel { get => ViewModel; set => ViewModel = value as DashboardViewModel; }

        public SeriesCollection LastHourSeries { get; set; }

        public DashboardPage(DashboardViewModel viewModel) : base(viewModel)
        {
            InitializeComponent();
            Loaded += OnPageLoad;
            var sales = new ChartValues<DateTimePoint>();
            var expenses = new ChartValues<DateTimePoint>();
            sales.AddRange(viewModel.Sales.Select(s => new DateTimePoint(s.Date, (double)s.Amount)));
            expenses.AddRange(viewModel.Expenses.Select(s => new DateTimePoint(s.Date, (double)s.Amount)));
            Chart2.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = Translations.Sales,
                    LabelPoint=c=>c.Y.ToString("C"),
                    Values = sales,
                    Fill = null,
                    LineSmoothness=0,
                    //PointForeground= new SolidColorBrush(AppManager._paletteHelper.GetTheme().PrimaryDark.Color),
                    PointForeground=  Brushes.Green,
                    PointGeometry=DefaultGeometries.Diamond,
                    PointGeometrySize=20,
                    Stroke = Brushes.Green,
                    //Stroke = new SolidColorBrush(AppManager._paletteHelper.GetTheme().PrimaryDark.Color),
                    StrokeThickness=4
                },

                new LineSeries
                {
                    Title = Translations.Purchases,
                    LabelPoint=c=>c.Y.ToString("C"),
                    Values =expenses,
                    Fill = null,
                    LineSmoothness=1,
                    PointForeground =  Brushes.Orange,
                    PointGeometrySize=15,
                    Stroke= Brushes.Orange,
                    StrokeThickness=4
                }
            };

            AxisX.LabelFormatter = val => new DateTime((long)val).ToString("dd MMM");
            AxisY.LabelFormatter = val => val.ToString("C2");
        }

        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            Unloaded -= OnPageUnloaded;
            Loaded -= OnPageLoad;
            ViewModel?.Dispose();
            ViewModel = null;
            IsDisposed = true;
        }

        private void OnPageLoad(object sender, RoutedEventArgs e)
        {
            Unloaded += OnPageUnloaded;
        }
    }
}
