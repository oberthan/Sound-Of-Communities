using System;
using System.Globalization;
using System.Windows.Data;

namespace MeshWave.Wpf.Converters;

public class ProgressToXConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double progress && parameter is string paramStr && double.TryParse(paramStr, out double width))
        {
            return (progress / 100.0) * width;
        }
        return 0.0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
