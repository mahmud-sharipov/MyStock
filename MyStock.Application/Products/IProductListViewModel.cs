namespace MyStock.Application.Products
{
    public interface IProductListViewModel : IEntityListPageViewModel<Product>
    {
        string NameSearchText { get; set; }
    }
}
