namespace MyStock.Dialogs
{
    public partial class ProductCategoryDialog : UserControl
    {
        public ProductCategoryDialog()
        {
            InitializeComponent();
            CategorySelector.Items.SortDescriptions.Add(
            new System.ComponentModel.SortDescription("Name",
            System.ComponentModel.ListSortDirection.Ascending));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ErrorMessages.Text = "";
            ErrorMessages.Visibility = Visibility.Collapsed;
            if (DataContext is ProductCategoryDTO category)
            {
                if (string.IsNullOrEmpty(category.Name))
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
