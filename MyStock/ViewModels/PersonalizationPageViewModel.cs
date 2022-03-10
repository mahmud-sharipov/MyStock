using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows.Media;

namespace MyStock.ViewModels;

public class PersonalizationPageViewModel : ReactiveObject
{
    private bool _isDarkTheme;
    private Color? _selectedColor;

    public ICommand ChangeHueCommand { get; }
    public ICommand ChangeToBackgroundCommand { get; }
    public ICommand ChangeToForegroundCommand { get; }

    public PersonalizationPageViewModel()
    {
        _isDarkTheme = AppManager.UISettings.Theme == UITheme.Dark;
        _selectedColor = AppManager.UISettings.Background;

        ChangeHueCommand = ReactiveCommand.Create<Color>(ChangeHue);
    }

    public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set
        {
            if (SetProperty(ref _isDarkTheme, value))
                AppManager.SetTheme(value ? UITheme.Dark : UITheme.Light);
        }
    }

    // public ColorScheme ActiveScheme { get => _activeScheme; set => SetProperty(ref _activeScheme, value); }

    public Color? SelectedColor
    {
        get => _selectedColor;
        set
        {
            if (SetProperty(ref _selectedColor, value) && value is { } color)
                AppManager.ChangeColor(color);
        }
    }

    private void ChangeHue(Color hue)
    {
        SelectedColor = hue;
        AppManager.ChangeColor(hue);
    }

    #region INotifyPropertyChanged
    protected virtual bool SetProperty<T>(ref T member, T value, [CallerMemberName] string propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(member, value))
        {
            return false;
        }

        member = value;
        this.RaisePropertyChanged(propertyName);
        return true;
    }
    #endregion
}