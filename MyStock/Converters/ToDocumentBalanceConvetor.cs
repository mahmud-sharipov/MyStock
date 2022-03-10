using System.Globalization;
using System.Windows.Data;

namespace MyStock.Converters
{
    public class ToDocumentBalanceConvetor : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var mutableNfi = (NumberFormatInfo)culture.NumberFormat.Clone();
            mutableNfi.CurrencySymbol = "";
            if (values.Length == 1 && decimal.TryParse(values[0].ToString(), out decimal param1))
                return $"${param1.ToString("C2", mutableNfi)}";
            else if (values.Length == 2 && decimal.TryParse(values[0].ToString(), out param1) && decimal.TryParse(values[1].ToString(), out decimal param2))
                return $"${(param1 - param2).ToString("C2", mutableNfi)}";

            return $"${0.ToString("C2", mutableNfi)}";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class EnumToBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return ((Enum)value).Equals((Enum)parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.Equals(true) ? parameter : Binding.DoNothing;
        }
    }
}
