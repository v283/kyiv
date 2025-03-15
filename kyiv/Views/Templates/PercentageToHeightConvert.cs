using System.Collections.ObjectModel;
using System.Globalization;

namespace kyiv.Views.Templates
{
    public class PercentageToHeightConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object Parameter, CultureInfo culture)
        {
            if (value is double percentage)
            {
                return percentage * 100;
            }
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}