namespace MyStock.Application.Vendors
{
    public interface IVendorListViewModel : IEntityListPageViewModel<Vendor>
    {
        public string NameSearch { get; set; }
    }
}
