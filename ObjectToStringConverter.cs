using System;
using System.Globalization;
using System.Windows.Data;

namespace SystemParametersViewer
{
    internal class ObjectToStringConverter : IValueConverter
    {
        public static string Convert(object value) => value?.ToString() ?? string.Empty;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();
    }
}
