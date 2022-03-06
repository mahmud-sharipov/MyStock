using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MyStock.Application.Dashboard;
using MyStock.Core.Interfaces;
using System.Drawing;
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

            Chart2.Series = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Sales",
                    LabelPoint=c=>c.Y.ToString("C"),
                    Values = GetData(),
                    Fill = null,
                    LineSmoothness=0,
                    PointForeground=System.Windows.Media.Brushes.Purple,
                    PointGeometry=DefaultGeometries.Diamond,
                    PointGeometrySize=20,
                    Stroke = System.Windows.Media.Brushes.Purple,
                    StrokeThickness=4
                },

                new LineSeries
                {
                    Title = "Expenses",
                    LabelPoint=c=>c.Y.ToString("C"),
                    Values = GetData(),
                    Fill = null,
                    LineSmoothness=1,
                    PointForeground=System.Windows.Media.Brushes.Red,
                    PointGeometrySize=15,
                    Stroke=System.Windows.Media.Brushes.Red,
                    StrokeThickness=4
                }
            };

            AxisX.LabelFormatter = val => new DateTime((long)val).ToString("dd MMM");
            AxisY.LabelFormatter = val => val.ToString("C");
        }

        private ChartValues<DateTimePoint> GetData()
        {
            var r = new Random();
            var trend = 50;
            var values = new ChartValues<DateTimePoint>();

            for (var i = 0; i < 50; i++)
            {
                var seed = r.NextDouble();
                if (seed > .8) trend += seed > .9 ? 50 : -50;
                values.Add(new DateTimePoint(DateTime.Now.AddDays(i), trend + r.Next(0, 10)));
            }

            return values;
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
