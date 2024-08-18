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
            : GetValue(values[0] is true ? ifExtension.TrueContent : ifExtension.FalseContent);

        object? GetValue(object? value)
        {
            if (value is I18nBinding binging &&
                binging.Converter?.Convert(new List<object?>() { culture, binging.Key }, null, null, culture)
                    is string strValue)
            {
                return strValue;
            }

            return value;
        }
    }
}
