namespace MyStock.Application.Products.Mapping;

public class ProductMappingProfile : MappingProfile<Product, ProductViewModel>
{
    public ProductMappingProfile() : base()
    {
        EntityToViewModelMapper.ForMember(vm => vm.StockLevels, opt => opt.Ignore());
        ViewModelToEntityMapper.ForMember(vm => vm.StockLevels, opt => opt.Ignore());
    }
}
