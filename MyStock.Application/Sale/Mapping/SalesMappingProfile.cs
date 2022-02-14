namespace MyStock.Application.Sale.Mapping
{
    public class SalesMappingProfile : MappingProfile<Sales, SalesViewModel>
    {
        public SalesMappingProfile() : base()
        {
            EntityToViewModelMapper.ForMember(vm => vm.Details, opt => opt.Ignore());
            ViewModelToEntityMapper.ForMember(vm => vm.Details, opt => opt.Ignore());
        }
    }
}