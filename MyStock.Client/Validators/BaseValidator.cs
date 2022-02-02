using System.Collections;
using System.Reflection;

namespace MyStock.Validators;

using System.ComponentModel;

public class BaseValidator<T> : FluentValidation.AbstractValidator<T>, IValidator
{
    public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    private readonly Dictionary<string, string> _errors = new Dictionary<string, string>();
    public IList<string> GetAllErrors() => _errors.Select(error => error.Value).ToList();
    public bool HasErrors => _errors.Count > 0;

    public IEnumerable GetErrors(string propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return GetAllErrors();

        ThrowIfInvalidPropertyName(propertyName);

        return ExtractErrorMessageOf(propertyName);
    }

    IDictionary<string, string> IValidator.Validate<TSource>(TSource instance)
    {
        if (instance is not T tInstance)
            throw new ArgumentException(nameof(instance));

        var currentErrors = new Dictionary<string, string>(_errors);
        ValidateAndUpdateErrors(tInstance);
        RaiseErrorsChangedIfReallyChanged(currentErrors, _errors);
        RaiseErrorsChangedIfReallyChanged(_errors, currentErrors);
        return _errors;
    }

    private static void ThrowIfInvalidPropertyName(string propertyName)
    {

        var propertyInfo = typeof(T).GetRuntimeProperty(propertyName);
        if (propertyInfo == null)
        {
            var msg = string.Format("No such property name '{0}' in {1}", propertyName, typeof(T));
            throw new ArgumentException(msg, propertyName);
        }
    }

    private void ValidateAndUpdateErrors(T instance)
    {
        _errors.Clear();
        var result = Validate(instance);
        if (result.IsValid)
            return;

        foreach (var err in result.Errors)
            _errors.Add(err.PropertyName, err.ErrorMessage);
    }

    private void RaiseErrorsChangedIfReallyChanged(Dictionary<string, string> errors1, Dictionary<string, string> errors2)
    {
        foreach (var err in errors1)
        {
            string propertyName = err.Key, message = err.Value;
            if (!errors2.ContainsKey(propertyName) || !errors2[propertyName].Equals(message))
                RaiseErrorsChanged(propertyName);
        }
    }

    private void RaiseErrorsChanged(string propertyName) =>
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));

    private IEnumerable ExtractErrorMessageOf(string propertyName)
    {
        var result = new List<string>();
        if (_errors.ContainsKey(propertyName))
            result.Add(_errors[propertyName]);

        return result;
    }
}
