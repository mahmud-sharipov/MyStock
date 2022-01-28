namespace MyStock.Dialogs;

public partial class UomDialog : UserControl
{
    public UomDialog()
    {
        InitializeComponent();
    }

    private void Save_Button_Click(object sender, RoutedEventArgs e)
    {
        ErrorMessages.Text = "";
        ErrorMessages.Visibility = Visibility.Collapsed;
        if (DataContext is Uom uom)
        {
            if (string.IsNullOrEmpty(uom.Name))
                ErrorMessages.Text += "Name is required!\r\n";

            if (string.IsNullOrEmpty(uom.Code))
                ErrorMessages.Text += "Code is required!\r\n";

            if (!string.IsNullOrEmpty(ErrorMessages.Text))
            {
                ErrorMessages.Visibility = Visibility.Visible;
                return;
            }
        }

        DialogHost.CloseDialogCommand.Execute(true, null);
    }

    private void Cancel_Button_Click(object sender, RoutedEventArgs e)
    {
        DialogHost.CloseDialogCommand.Execute(false, null);
    }
}
