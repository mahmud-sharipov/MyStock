using MyStock.Application.Products.Pages;

namespace MyStock.Application.Products
{
    public class ProductListViewModel : EntityListPageViewModel<Product, IProductListEntityPage>, IProductListViewModel
    {
        private string nameSearchText;

        public ProductListViewModel(IContext context) : base(context)
        {
            Title = Translations.Products;
        }

        public string NameSearchText
        {
            get => nameSearchText;
            set => this.RaiseAndSetIfChanged(ref nameSearchText, value, nameof(NameSearchText), nameof(Collection));
        }

        public override bool CanDeleteEntity(Product entity, out string reason)
        {
            reason = "";
            if (entity.DocumentDetails.Any())
            {
                reason = Translations.ProductHasDocumentMessage;
                return false;
            }
            return true;
        }

        protected override ICollection<ColumnViewModel> BuildColums()
        {
            return new List<ColumnViewModel>()
            {
                new (Translations.Code, nameof(Product.Code),1),
                new (Translations.Description, nameof(Product.Description),2),
                new (Translations.Category, $"{nameof(Product.Category)}.{nameof(ProductCategory.Name)}" ,3),
                new (Translations.UOM, $"{nameof(Product.Uom)}.{nameof(Uom.Name)}" ,4),
                new (Translations.Price, nameof(Product.Price),5),
                new (Translations.Cost, nameof(Product.Cost),6),
                new (Translations.Active, nameof(Product.IsActive),7, ColumnType.CheckBox),
            };
        }

        protected override IViewable CreateEntityViewModel(Product entity) => new ProductViewModel(entity, Context);

        protected override bool FilereItem(Product entity)
        {
            return string.IsNullOrEmpty(NameSearchText) || entity.Description.Contains(NameSearchText, StringComparison.CurrentCultureIgnoreCase);
        }

        protected override Product CreateNewEntity()
        {
            var product = new Product() { IsActive = true };
            foreach (var warehouse in Context.Set<Warehouse>())
            {
                var sl = new ProductStockLevel() { Warehouse = warehouse, Product = product };
                Context.AddToContext(sl);
                product.StockLevels.Add(sl);
            }
            return product;
        }
    }

}
