using MyStock.Application.Category;
using MyStock.Application.Products.Pages;
using MyStock.Core.Report;

namespace MyStock.Application.Products
{
    public class ProductListViewModel : EntityListPageViewModel<Product, IProductListEntityPage>, IProductListViewModel
    {
        private string nameSearchText;
        private ProductStockStatus _nameSearchText;
        private bool _includeInactive;

        public ProductListViewModel(IContext context) : base(context)
        {
            Title = Translations.Products;
            IncludeInactive = false;
            StockStatus = ProductStockStatus.All;
        }

        public string NameSearchText
        {
            get => nameSearchText;
            set => this.RaiseAndSetIfChanged(ref nameSearchText, value, nameof(NameSearchText), nameof(Collection));
        }

        public ProductStockStatus StockStatus
        {
            get => _nameSearchText;
            set => this.RaiseAndSetIfChanged(ref _nameSearchText, value, nameof(StockStatus), nameof(Collection));
        }

        public bool IncludeInactive
        {
            get => _includeInactive;
            set => this.RaiseAndSetIfChanged(ref _includeInactive, value, nameof(StockStatus), nameof(Collection));
        }

        public ICommand Report { get; set; }
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
                //new (Translations.Cost, nameof(Product.Cost),6,"C2"),
                new (Translations.Active, nameof(Product.IsActive),7, ColumnType.CheckBox),
            };
        }

        protected override IViewable CreateEntityViewModel(Product entity) => new ProductViewModel(entity, Context);

        protected override Expression<Func<Product, bool>> FilereItem()
        {
            Expression<Func<Product, bool>> res = e => e.IsActive || IncludeInactive;
            if (SelectedCategory != null)
                res = e => (e.IsActive || IncludeInactive) && SelectedCategoryTreeGuids.Contains(e.CategoryGuid);

            Expression<Func<Product, bool>> stock = StockStatus switch
            {
                ProductStockStatus.OutOfStock => e => e.StockLevels.Any(s => s.MaxQuantity > 0 && s.NetQuantity <= 0),
                ProductStockStatus.LowStock => e => e.StockLevels.Any(s => s.MaxQuantity > 0 && s.NetQuantity <= s.MinQuantity),
                ProductStockStatus.OverStock => e => e.StockLevels.Any(s => s.MaxQuantity > 0 && s.NetQuantity > s.MaxQuantity),
                _ => e => true,
            };

            var param = Expression.Parameter(typeof(Product), "e");
            var body = Expression.AndAlso(
                Expression.Invoke(res, param),
                Expression.Invoke(stock, param));

            return Expression.Lambda<Func<Product, bool>>(body, param);
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

            Report = ReactiveCommand.Create(async () =>
            {
                await Task.Run(() =>
                {
                    ReportBuilder.New(@"Assets\Reports\ProductInfo.html")
                    .UseDefaultStyles()
                    .AddParameter(ReportGlobalConsts.ConpanyName, Global.Settings.CompanyName)
                    .AddParameter("$Products$", string.Join("", _collection.Select(GetPrductInfoForReport)))
                    .GenerateAndOpen(@"D:/Test.pdf");
                });
            }, outputScheduler: Scheduler.CurrentThread);
        }

        protected string GetPrductInfoForReport(Product product, int index)
        {
            var sl = product.StockLevels.First();
            return $@"<tr>
                        <td>{++index}</td>
                        <td>{product.Code}</td>
                        <td>{product.Description}</td>
                        <td>{product.Category.Name}</td>
                        <td>{product.Price:C2}</td>
                        <td>{sl.MinQuantity:C2}</td>
                        <td>{sl.MaxQuantity:C2}</td>
                        <td>{sl.NetQuantity:C2}</td>
                      </tr>";
        }

        void GetCategoryTree(ProductCategory category, List<Guid> guids)
        {
            if (category == null) return;
            guids.Add(category.Guid);
            foreach (var item in category.ChildCategories)
                GetCategoryTree(item, guids);
        }
    }

    public enum ProductStockStatus
    {
        All,
        OutOfStock,
        LowStock,
        OverStock
    }
}
