namespace MyStock.Application.Settings;

public interface ISettingsViewModel : IViewModel, INavigatable
{
    ObservableCollection<ProductCategory> ProductCategories { get; }
}
