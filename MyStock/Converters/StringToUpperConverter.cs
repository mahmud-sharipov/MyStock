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

    public class NumberToQuantityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (decimal.TryParse(value.ToString(), out decimal result))
            {
                var mutableNfi = (NumberFormatInfo)culture.NumberFormat.Clone();
                mutableNfi.CurrencySymbol = "";
                return result.ToString("C2", mutableNfi);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }

    class ToTotalPriceConvetor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var mutableNfi = (NumberFormatInfo)culture.NumberFormat.Clone();
            mutableNfi.CurrencySymbol = "";
            if (values.Length == 1 && decimal.TryParse(values[0].ToString(), out decimal param1))
                return $"${param1.ToString("C2", mutableNfi)}";
            else if (values.Length == 2 && decimal.TryParse(values[0].ToString(), out param1) && decimal.TryParse(values[1].ToString(), out decimal param2))
                return $"${(param1 * param2).ToString("C2", mutableNfi)}";

            return $"${0.ToString("C2", mutableNfi)}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
