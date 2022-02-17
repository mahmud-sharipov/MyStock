namespace MyStock.Application.Purchases.Mapping
{
    public class PurchaseMappingProfile : MappingProfile<Purchase, PurchaseViewModel>
    {
        public PurchaseMappingProfile() : base()
        {
            EntityToViewModelMapper.ForMember(vm => vm.Details, opt => opt.Ignore());
            ViewModelToEntityMapper.ForMember(vm => vm.Details, opt => opt.Ignore());
        }
    }
}