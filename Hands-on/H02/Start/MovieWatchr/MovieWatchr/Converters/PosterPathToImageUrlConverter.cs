using System;
using Windows.UI.Xaml.Data;

namespace MovieWatchr.Converters
{
    public class PosterPathToImageUrlConverter : IValueConverter
    {
        private string ImageBaseUrl = "http://image.tmdb.org/t/p/";

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !(value is string)) return string.Empty;

            return string.Format("{0}{1}{2}", ImageBaseUrl, parameter == null ? "w92" : parameter, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
