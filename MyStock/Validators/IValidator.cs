namespace MyStock.Validators;

using System.ComponentModel;

public interface IValidator : INotifyDataErrorInfo
{
    IList<string> GetAllErrors();
    IDictionary<string, string> Validate<TSource>(TSource instance);
}
