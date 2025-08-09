using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using AvaloniaExtensions.Axaml.Markup;

// ReSharper disable once CheckNamespace
namespace AvaloniaExtensions.Axaml.Converters;

public class IfConverter(IfBinding ifExtension) : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        return values.Count <= 0
            ? BindingOperations.DoNothing
            : (values[0] is true ? ifExtension.TrueContent : ifExtension.FalseContent);
    }
}
