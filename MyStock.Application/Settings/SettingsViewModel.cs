namespace MyStock.Application.Settings
{
    public class SettingsViewModel : ViewModel, ISettingsViewModel
    {

        public SettingsViewModel(IContext context) : base(context)
        {

        }
        protected ObservableCollection<ProductCategory> _productCategories;
        public ObservableCollection<ProductCategory> ProductCategories
        {
            get
            {
                if (_productCategories == null)
                    _productCategories = new ObservableCollection<ProductCategory>(Context.Set<ProductCategory>().ToList());
                return _productCategories;
            }
        }

        ISettingsPage _entityPage;
        public ISettingsPage EntityPage =>
            _entityPage ??= Global.Container.Resolve<ISettingsPage>(new PositionalParameter(0, this));

        IEntityListPage INavigatable.EntityPage => EntityPage;
    }
}
