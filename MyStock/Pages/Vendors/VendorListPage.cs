using MyStock.Application.Vendors;
using MyStock.Application.Vendors.Pages;

namespace MyStock.Pages.Vendors
{
    public class VendorListPage : EntityListPage<VendorListViewModel>, IVendorListEntityPage
    {
        public VendorListPage(VendorListViewModel viewModel) : base(viewModel)
        {
            //var filter = new UomListFilter();
            //filter.NameSearchTextBox.SetBinding(TextBox.TextProperty, new Binding(nameof(ViewModel.NameSearchText)) { Source = ViewModel, UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged });
            //ColletionFilters = filter;
            InitializeDefaulPage();
        }
    }
}
