using Autofac;
using AutoMapper;
using MyStock.Application.AutoMapper;
using MyStock.Application.UIComunication;
using MyStock.Core.Interfaces;
using MyStock.Persistence.Database;

namespace MyStock.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            BuildContext(builder);
            BuildUIComunication(builder);
            BuildValidation(builder);
            RegisterMaps(builder);
            RegisterViewModels(builder);
        }

        static void BuildContext(ContainerBuilder builder)
        {
            builder.RegisterType<MyStockContext>().As<IContext>().InstancePerDependency();
        }

        static void BuildUIComunication(ContainerBuilder builder)
        {
            builder.RegisterType<Message>().As<IMessage>().InstancePerLifetimeScope();
        }

        private static void RegisterMaps(ContainerBuilder builder)
        {
            builder.Register(c => AutoMapperConfig.RegisterMappings()).As<IMapper>().InstancePerLifetimeScope();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<MyStock.Application.Uoms.UomListViewModel>()
                .As<MyStock.Application.Uoms.IUomListViewModel>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Products.ProductListViewModel>()
                .As<MyStock.Application.Products.IProductListViewModel>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Customers.CustomerListViewModel>()
                .As<MyStock.Application.Customers.ICustomerListViewModel>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Vendors.VendorListViewModel>()
                .As<MyStock.Application.Vendors.IVendorListViewModel>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Sale.SalesListViewModel>()
                .As<MyStock.Application.Sale.ISalesListViewModel>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Purchases.PurchaseListViewModel>()
                .As<MyStock.Application.Purchases.IPurchaseListViewModel>().InstancePerDependency();
        }

        private static void BuildValidation(ContainerBuilder builder)
        {
            builder.RegisterType<MyStock.Application.Uoms.Validators.UomValidator>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Products.Validators.ProductValidator>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.StockLevels.ProductStockLevelViewModelValidator>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Customers.Validators.CustomerValidator>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Vendors.Validators.VendorValidator>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Sale.Validators.SalesValidator>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Documents.Validators.DocumentDetailValidator>().InstancePerDependency();
            builder.RegisterType<MyStock.Application.Purchases.Validators.PurchaseValidator>().InstancePerDependency();
        }
    }
}
