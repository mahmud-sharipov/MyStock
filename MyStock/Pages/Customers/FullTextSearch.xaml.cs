using System.Linq;

namespace MyStock.Pages.Customers
{
    public partial class CustomerFullTextSearch : UserControl
    {
        public CustomerFullTextSearch()
        {
            InitializeComponent();
        }

        private void uiSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            resultPopup.IsOpen = true;
        }

        private void uiSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(uiSearch.Text))
                ResultList.SelectedItem = null;
            else if (uiSearch.Text != ResultList.SelectedItem?.ToString())
            {
                foreach (var item in ResultList.ItemsSource)
                {
                    if (item == ResultList.SelectedItem)
                        uiSearch.Text = item.ToString();
                    else
                        ResultList.SelectedItem = item;
                    break;
                }
            }
            resultPopup.IsOpen = false;
        }
    }
}
