using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MovieWatchr.Converters
{
    public class IntegerToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is int)) return Visibility.Collapsed;
            int intValue = (int)value;

            if (intValue != -1) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
