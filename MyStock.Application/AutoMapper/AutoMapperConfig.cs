using AutoMapper;
using MyStock.Application.Products.Mapping;
using MyStock.Application.StockLevels;
using MyStock.Application.Uoms.Mapping;

namespace MyStock.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static IMapper RegisterMappings()
        {
            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UomMappingProfile());
                cfg.AddProfile(new ProductMappingProfile());
                cfg.AddProfile(new ProductStockLevelMappingProfile());
                //cfg.AddProfile(new CategorizedEntityAndCategorizedEntityViewModelMappingProfile<CategorizedEntity, ICategorizedEntityViewModel>());
                //cfg.AddProfile(new CategoryLinkAndCategoryLinkViewModelMappingProfile());
                //cfg.AddProfile(new CategoryAndCategoryViewModelMappingProfile<Category, ICategoryViewModel>());
                //cfg.AddProfile(new ContactAndContactViewModelMappingProfile());
                //cfg.AddProfile(new PriceFormulaAndPriceFormulaViewModelMappingProfile<PriceFormula, IPriceFormulaViewModel>());

                //cfg.AddProfile(new VendorToVendorViewModelMappingProfile());
                //cfg.AddProfile(new ProductToProductViewModelMappingProfile());
                //cfg.AddProfile(new InventoryDocDetailAndInventoryDocDetailViewModelMappingProfile<InventoryDocDetail, IInventoryDocDetailViewModel>());
                //cfg.AddProfile(new InventoryDocAndInventoryDocViewModelMappingProfile<InventoryDoc, IInventoryDocViewModel>());
                //cfg.AddProfile(new UserToUserViewModelMappingProfile());
                //cfg.AddProfile(new ProductCategoryToProductCategoryViewModelMappingProfile());
                //cfg.AddProfile(new ProductAlternativeToProductAlternativeViewModelMappingProfile());
                //cfg.AddProfile(new UnitOfMeasureToUnitOfMeasureViewModelMappingProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}
