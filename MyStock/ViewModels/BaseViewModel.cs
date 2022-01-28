using MyStock.Validators;
using System.Collections;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace MyStock.ViewModels;

public abstract class BaseViewModel : IDisposable, INotifyPropertyChanged
{
    MyStockContext _context;
    public MyStockContext Context
    {
        get
        {
            if (_context == null)
                _context = new MyStockContext();
            return _context;
        }
        init => _context = value;
    }

    protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
    {
        if (!EqualityComparer<T>.Default.Equals(field, newValue))
        {
            field = newValue;
            RaisePropertyChanged(propertyName);
            return true;
        }

        return false;
    }

    #region INotifyPropertyChanged

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression) =>
        RaisePropertyChanged(propertyExpression.ExtractPropertyName());

    protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) =>
        PropertyChanged?.Invoke(this, e);

    protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    #endregion

    #region IDisposable

    public void Dispose() =>
            OnDispose();

    protected virtual void OnDispose() { }

    #endregion
}

public abstract class BaseViewModel<TValidator> : BaseViewModel, INotifyDataErrorInfo
    where TValidator : class, IValidator
{
    private readonly TValidator _validator;

    public BaseViewModel(TValidator validator)
    {
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _validator.ErrorsChanged += OnErrorsChanged; ;

    }

    public BaseViewModel(TValidator validator, MyStockContext context) : this(validator)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void ValidateAllProperties() => _validator.Validate(this);

    protected virtual bool SetPropertyAndValidateAllProperties<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
        if (SetProperty(ref storage, value, propertyName))
        {
            ValidateAllProperties();
            return true;
        }

        return false;
    }

    #region INotifyDataErrorInfo

    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

    public bool HasErrors => _validator.HasErrors;

    private void OnErrorsChanged(object sender, DataErrorsChangedEventArgs e) =>
        ErrorsChanged?.Invoke(this, e);

    public virtual IEnumerable GetErrors(string propertyName)
    {
        return _validator.GetErrors(propertyName);
    }

    public IList<string> GetAllErrors() => _validator.GetAllErrors();
    #endregion

}