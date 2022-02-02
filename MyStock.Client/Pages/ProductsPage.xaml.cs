namespace MyStock.Pages;

public partial class ProductsPage
{
    public ProductsPage() : base(new ProductsViewModel())
    {
        InitializeComponent();
        TreeTypes.Items.SortDescriptions.Add(
            new System.ComponentModel.SortDescription("Name",
            System.ComponentModel.ListSortDirection.Ascending));
    }
}