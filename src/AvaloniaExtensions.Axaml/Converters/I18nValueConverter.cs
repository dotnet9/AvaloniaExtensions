using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace AvaloniaExtensions.Axaml.Converters;

public class I18nValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Get(value);
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }

    public static object? Get(object? value, bool trimEnd = false)
    {
        return value switch
        {
            string @string => trimEnd ? @string.TrimEnd() : @string,
            _ => value
        };
    }
}
