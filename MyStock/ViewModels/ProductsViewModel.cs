using MyStock.Assets.Lang;

namespace MyStock.ViewModels;

public class ProductsViewModel : BaseViewModel
{
    public ProductsViewModel() : base()
    {

    }

    #region Properties
    ObservableCollection<Product> _products;
    public ObservableCollection<Product> Products
    {
        get
        {
            if (_products == null)
                _products = new ObservableCollection<Product>(Context.Set<Product>());
            return _products;
        }
    }

    ObservableCollection<ProductCategory> _productCategories;
    public ObservableCollection<ProductCategory> ProductCategories
    {
        get
        {
            if (_productCategories == null)
                _productCategories = new ObservableCollection<ProductCategory>(Context.Set<ProductCategory>());
            return _productCategories;
        }
    }
    #endregion

    #region Commands

    public RelayCommand AddCategory => new RelayCommand(async param =>
    {
        var categoryDto = new ProductCategoryDTO() { AvailableCategories = ProductCategories };
        if (await DialogManger.Show<ProductCategoryDialog, ProductCategoryDTO>(categoryDto))
        {
            var category = new ProductCategory()
            {
                Name = categoryDto.Name,
                Parent = categoryDto.Parent
            };
            Context.Add(category);
            Context.SaveChanges();
            ProductCategories.Add(category);
        }
    });

    public RelayCommand UpdateCategory => new RelayCommand(async param =>
    {
        if (param is ProductCategory category)
        {
            var dto = new ProductCategoryDTO()
            {
                Name = category.Name,
                Parent = category.Parent,
                AvailableCategories = ProductCategories
            };

            if (await DialogManger.Show<ProductCategoryDialog, ProductCategoryDTO>(dto))
            {
                category.Name = dto.Name;
                category.Parent = dto.Parent == category ? null : dto.Parent;
                Context.Update(category);
                Context.SaveChanges();
            }
        }
    });

    public RelayCommand DeleteCategory => new RelayCommand(param =>
    {
        if (param is ProductCategory category)
        {
            if (category.Products.Any() || category.ChildCategories.Any())
                NotificationManager.Error(Lang.ProductCategoryHasProductAndOrChildCategoriesMessage.Format(category.Name));
            else
            {
                Context.Delete(category);
                ProductCategories.Remove(category);
                Context.SaveChanges();
                RaisePropertyChanged(nameof(ProductCategories));
            }
        }
    });

    public RelayCommand AddProduct => new RelayCommand(async param =>
    {
        var productDto = new ProductDto(new Product())
        {
            IsActive = true,
            AvailableCategories = ProductCategories,
            AvailableUoms = Context.Set<Uom>().ToList()
        };

        if (await DialogManger.Show<ProductDialog, ProductDto>(productDto))
        {
            var product = productDto.GetProduct();
            Context.Add(product);
            Context.SaveChanges();
            Products.Add(product);
        }
    });

    public RelayCommand UpdateProduct => new RelayCommand(async param =>
    {
        if (param is Product product)
        {
            var productDto = new ProductDto(product)
            {
                AvailableCategories = ProductCategories,
                AvailableUoms = Context.Set<Uom>().ToList()
            };

            if (await DialogManger.Show<ProductDialog, ProductDto>(productDto))
            {
                _ = productDto.GetProduct();
                Context.Update(product);
                Context.SaveChanges();
            }
        }
    });

    public RelayCommand DeleteProduct => new RelayCommand(param =>
    {
        if (param is Product product)
        {
            if (product.DocumentDetails.Any())
                NotificationManager.Error(Lang.ProductHasDocumentMessage.Format(product.Name));
            else
            {
                Context.Delete(product);
                Products.Remove(product);
                Context.SaveChanges();
            }
        }
    });

    #endregion

    protected override void OnDispose()
    {
        _products = null;
        _productCategories = null;
        base.OnDispose();
    }
}

public class ProductCategoryDTO
{
    public string Name { get; set; }
    public ProductCategory Parent { get; set; }
    public ObservableCollection<ProductCategory> AvailableCategories { get; set; }
}

public class ProductDto
{
    private readonly Product _product;

    public ProductDto(Product product)
    {
        Name = product.Name;
        Description = product.Description;
        Category = product.Category;
        Uom = product.Uom;
        Code = product.Code;
        Price = product.Price;
        IsActive = product.IsActive;
        Cost = product.Cost;
        _product = product;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public string Code { get; set; }
    public decimal Price { get; set; }
    public decimal Cost { get; set; }
    public bool IsActive { get; set; }
    public Uom Uom { get; set; }
    public ProductCategory Category { get; set; }
    public IEnumerable<ProductCategory> AvailableCategories { get; set; }
    public IEnumerable<Uom> AvailableUoms { get; set; }

    internal Product GetProduct()
    {
        _product.Name = Name;
        _product.Description = Description;
        _product.Code = Code;
        _product.Price = Price;
        _product.IsActive = IsActive;
        _product.Cost = Cost;
        _product.Category = Category;
        _product.Uom = Uom;
        return _product;
    }
}
