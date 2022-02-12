using System.Windows.Threading;

namespace MyStock.Controls
{
    public partial class DigitalClock : UserControl
    {

        DispatcherTimer timer;

        public DigitalClock()
        {
            InitializeComponent();
            timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            timer.Tick += timer_Tick;
            timer.Start();

            Unloaded += DigitalClock_Unloaded;
        }

        private void DigitalClock_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Tick -= timer_Tick;
            Unloaded -= DigitalClock_Unloaded;
            timer = null;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToLongTimeString();
        }
    }
}
