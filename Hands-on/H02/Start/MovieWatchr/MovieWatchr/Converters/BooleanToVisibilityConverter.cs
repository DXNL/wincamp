using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MovieWatchr.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is bool)) return Visibility.Collapsed;
            bool boolValue = (bool)value;

            if (boolValue != false) return Visibility.Visible;

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
