using System;
using System.Collections;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AvaloniaExtensions.Axaml.Converters;

public class IsCollectionEmptyConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value switch
        {
            null => true,
            IList list => list.Count == 0,
            _ => false
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
