using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SystemParametersViewer
{
    internal class ObjectToColorConverter : IValueConverter
    {
        public static Color Convert(object value) => value is Color color ? color : Color.FromArgb(0, 0, 0, 0);

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => Convert(value);

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => value;
    }
}
