using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace nmbstrRssReader.Converters
{
    public sealed class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo language)
        {
            if (value is bool)
            {
                if (parameter != null && parameter.ToString().ToLowerInvariant() == "inverse")
                {
                    if ((bool)value)
                    {
                        return Visibility.Collapsed;
                    }
                    return Visibility.Visible;
                }
                if ((bool)value)
                {
                    return Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo language)
        {
            return value is Visibility && (Visibility)value == Visibility.Visible;
        }
    }
}
