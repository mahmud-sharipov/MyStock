using MyStock.Application.Assets.Lang;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace MyStock.Converters
{
    public class ProductRemainingStockLevelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Product product)
            {
                var remainingQuantity = product.StockLevels.FirstOrDefault()?.NetQuantity ?? 0;
                var mutableNfi = (NumberFormatInfo)culture.NumberFormat.Clone();
                return $"{Translations.OnHand}: {remainingQuantity.ToString("N2", mutableNfi)} {product.Uom.Code}";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
