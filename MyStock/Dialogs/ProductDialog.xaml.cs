using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyStock.Dialogs
{
    public partial class ProductDialog : UserControl
    {
        public ProductDialog()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessages.Text = "";
            ErrorMessages.Visibility = Visibility.Collapsed;
            if (DataContext is ProductDto dto)
            {
                if (string.IsNullOrEmpty(dto.Name))
                    ErrorMessages.Text += "Name is required!\r\n";

                if (!string.IsNullOrEmpty(ErrorMessages.Text))
                {
                    ErrorMessages.Visibility = Visibility.Visible;
                    return;
                }
            }

            DialogHost.CloseDialogCommand.Execute(true, null);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(false, null);
        }
    }

}
