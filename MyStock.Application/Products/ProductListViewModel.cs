using MyStock.Application.Category;
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

        public ICommand DeleteCategory { get; set; }
        public ICommand AddCategory { get; set; }
        public ICommand OpenCategory { get; set; }
        public ICommand ResetSelectedCategory { get; set; }

        protected ObservableCollection<ProductCategory> _productCategories;
        public ObservableCollection<ProductCategory> ProductCategories =>
            _productCategories ??= new ObservableCollection<ProductCategory>(Context.Set<ProductCategory>().Where(c => c.Parent == null).ToList());

        ProductCategory _selectedCategory;
        public ProductCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                SelectedCategoryTreeGuids = new List<Guid>();
                GetCategoryTree(value, SelectedCategoryTreeGuids);
                this.RaiseAndSetIfChanged(ref _selectedCategory, value, nameof(SelectedCategory), nameof(Collection));
            }
        }

        List<Guid> SelectedCategoryTreeGuids { get; set; }

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
                new (Translations.Price, nameof(Product.Price),5,"C2"),
                new (Translations.Cost, nameof(Product.Cost),6,"C2"),
                new (Translations.Active, nameof(Product.IsActive),7, ColumnType.CheckBox),
            };
        }

        protected override IViewable CreateEntityViewModel(Product entity) => new ProductViewModel(entity, Context);

        protected override bool FilereItem(Product entity)
        {
            if (string.IsNullOrEmpty(NameSearchText) && SelectedCategory == null)
                return true;

            if (SelectedCategory == null)
                return entity.Description.Contains(NameSearchText, StringComparison.CurrentCultureIgnoreCase);

            if (string.IsNullOrEmpty(NameSearchText))
                return SelectedCategoryTreeGuids.Contains(entity.CategoryGuid);

            return entity.Description.Contains(NameSearchText, StringComparison.CurrentCultureIgnoreCase) && SelectedCategoryTreeGuids.Contains(entity.CategoryGuid);
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

        protected override void BuildCommands()
        {
            base.BuildCommands();

            AddCategory = ReactiveCommand.Create(async () =>
            {
                var entity = new ProductCategory();
                var viewModel = new ProductCategoryViewModel(entity, Context);
                Context.AddToContext(entity);
                var result = await viewModel.DialogHost.Show(viewModel.EntityPage, IDialogHost.RootDialogIdentifier);
                if (true.Equals(result))
                    ProductCategories.Add(entity);
            }, CanAdd, outputScheduler: Scheduler.CurrentThread);

            OpenCategory = ReactiveCommand.Create<ProductCategory>((param) =>
            {
                var entity = param ?? SelectedCategory;
                if (entity != null)
                {
                    var viewModel = new ProductCategoryViewModel(entity, Context);
                    viewModel.DialogHost.Show(viewModel.EntityPage, IDialogHost.RootDialogIdentifier);
                }
            }, CanOpen, outputScheduler: Scheduler.CurrentThread);

            ResetSelectedCategory = ReactiveCommand.Create(() =>
            {
                SelectedCategory = null;
            }, outputScheduler: Scheduler.CurrentThread);

            DeleteCategory = ReactiveCommand.Create<ProductCategory>((param) =>
            {
                if (param is not ProductCategory entity) return;

                if (param.Products.Any() || param.ChildCategories.Any())
                {
                    var message = Global.Container.Resolve<IMessage>();
                    message.Detail = Translations.CategoryCannotBeDeleted;
                    message.Severity = SeverityLevel.Error;
                    message.Show();
                }
                else
                {
                    if (entity == SelectedCategory)
                    {
                        SelectedCategory = null;
                    }
                    Context.Delete(entity);
                    Context.SaveChanges();
                    ProductCategories.Remove(entity);
                }
            }, outputScheduler: Scheduler.CurrentThread);
        }

        void GetCategoryTree(ProductCategory category, List<Guid> guids)
        {
            if(category == null) return;
            guids.Add(category.Guid);
            foreach (var item in category.ChildCategories)
                GetCategoryTree(item, guids);
        }
    }
}
