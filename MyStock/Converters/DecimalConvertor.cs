using MyStock.Application.Assets.Lang;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MyStock.Converters
{
    public class DecimalConvertor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var result = 0m;
            if (string.IsNullOrEmpty(value?.ToString()))
                return result;
            decimal.TryParse(value.ToString(), out result);
            return result;
        }
    }
}
