using System.Globalization;
using System.Windows.Data;

namespace MyStock.Converters
{
    public class StringToUpperConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is string)
            {
                return ((string)value).ToUpper();
            }

            return value ?? "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ?? "";
        }
    }

    public class NumberToCurrencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (decimal.TryParse(value.ToString(), out decimal result))
            {
                var mutableNfi = (NumberFormatInfo)culture.NumberFormat.Clone();
                mutableNfi.CurrencySymbol = "";
                return $"${result.ToString("C2", mutableNfi)}";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
