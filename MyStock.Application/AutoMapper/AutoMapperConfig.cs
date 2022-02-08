using AutoMapper;

namespace MyStock.Application.AutoMapper;

public class AutoMapperConfig
{
    public static IMapper RegisterMappings()
    {
        var mappingConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MyStock.Application.Uoms.Mapping.UomMappingProfile());
            cfg.AddProfile(new MyStock.Application.Products.Mapping.ProductMappingProfile());
            cfg.AddProfile(new MyStock.Application.StockLevels.ProductStockLevelMappingProfile());
            cfg.AddProfile(new MyStock.Application.Customers.Mapping.CustomerMappingProfile());
            cfg.AddProfile(new MyStock.Application.Vendors.Mapping.VendorMappingProfile());
            cfg.AddProfile(new MyStock.Application.Sale.Mapping.SalesMappingProfile());
            cfg.AddProfile(new MyStock.Application.Documents.Mapping.DocumentDetailMappingProfile());
        });
        return mappingConfig.CreateMapper();
    }
}
