using Autofac;
using AutoMapper;
using MyStock.Application.AutoMapper;
using MyStock.Application.Products;
using MyStock.Application.Products.Validators;
using MyStock.Application.StockLevels;
using MyStock.Application.UIComunication;
using MyStock.Application.Uoms;
using MyStock.Application.Uoms.Validators;
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
            builder.RegisterType<UomListViewModel>().As<IUomListViewModel>().InstancePerDependency();
            builder.RegisterType<ProductListViewModel>().As<IProductListViewModel>().InstancePerDependency();
        }

        private static void BuildValidation(ContainerBuilder builder)
        {
            builder.RegisterType<UomValidator>().InstancePerDependency();
            builder.RegisterType<ProductValidator>().InstancePerDependency();
            builder.RegisterType<ProductStockLevelViewModelValidator>().InstancePerDependency();
        }
    }
}
