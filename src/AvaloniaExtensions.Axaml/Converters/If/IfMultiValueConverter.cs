using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using AvaloniaExtensions.Axaml.ExtensionMethods;
using AvaloniaExtensions.Axaml.Markup;

// ReSharper disable once CheckNamespace
namespace AvaloniaExtensions.Axaml.Converters;

public class IfMultiValueConverter(IfBindingExtension ifExtension) : IMultiValueConverter
{
    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        var condition = values[ifExtension.ConditionIndex];

        if (condition == AvaloniaProperty.UnsetValue) return BindingOperations.DoNothing;

        return condition.CastTo<bool>()
            ? GetValue(ifExtension.TrueIndex, ifExtension.TrueContent)
            : GetValue(ifExtension.FalseIndex, ifExtension.FalseContent);

        object? GetValue(int index, object? storage) => index != IfBindingExtension.InvalidIndex ? values[index] : storage;
    }
}
