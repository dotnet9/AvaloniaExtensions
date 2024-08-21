using System;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;

namespace AvaloniaExtensions.Axaml.Converters;

public class EnumToBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value?.Equals(parameter);

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        value?.Equals(true) == true ? parameter : BindingOperations.DoNothing;
}
