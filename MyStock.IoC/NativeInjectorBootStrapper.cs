using Autofac;
using AutoMapper;
using MyStock.Application.Uoms;
using MyStock.Core.Interfaces;
using MyStock.Persistence.Database;

namespace MyStock.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(ContainerBuilder builder)
        {
            BuildContext(builder);
            BuildValidation(builder);
            RegisterMaps(builder);
            RegisterViewModels(builder);
        }

        static void BuildContext(ContainerBuilder builder)
        {
            builder.RegisterType<MyStockContext>().As<IContext>().InstancePerDependency();
        }

        private static void RegisterMaps(ContainerBuilder builder)
        {
            //builder.Register(c => AutoMapperConfig.RegisterMappings()).As<IMapper>().InstancePerLifetimeScope();
        }

        private static void RegisterViewModels(ContainerBuilder builder)
        {
            builder.RegisterType<UomListViewModel>().As<IUomListViewModel>().InstancePerDependency();
        }

        private static void BuildValidation(ContainerBuilder builder)
        {
            //builder.RegisterType<AccountViewModelValidator>().As<IAccountViewModelValidator>().InstancePerDependency();
            //builder.RegisterType<CategorizedEntityViewModelValidator>().As<ICategorizedEntityViewModelValidator>().InstancePerDependency();
            //builder.RegisterType<CategoryViewModelValidator>().As<ICategoryViewModelValidator>().InstancePerDependency();
            //builder.RegisterType<CategoryLinkViewModelValidator>().As<ICategoryLinkViewModelValidator>().InstancePerDependency();
            //builder.RegisterType<ContactViewModelValidator>().As<IContactViewModelValidator>().InstancePerDependency();

            //builder.RegisterType<VendorViewModelValidation>().As<IVendorViewModelValidation>().InstancePerDependency();
            //builder.RegisterType<ProductViewModelValidator>().As<IProductViewModelValidator>().InstancePerDependency();
            //builder.RegisterType<ProductAlternativeViewModelValidator>().As<IProductAlternativeViewModelValidator>().InstancePerDependency();
            //builder.RegisterType<ProductCategoryViewModelValidator>().As<IProductCategoryViewModelValidator>().InstancePerDependency();
            //builder.RegisterType<PriceFormulaViewModelValidator>().As<IPriceFormulaViewModelValidator>().InstancePerDependency();
            //builder.RegisterType<UnitOfMeasureViewModelValidator>().As<IUnitOfMeasureViewModelValidator>().InstancePerDependency();
            //builder.RegisterType<UserViewModelValidation>().As<IUserViewModelValidation>().InstancePerDependency();
        }
    }
}
