using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using AvaloniaExtensions.Axaml.Markup;

namespace AvaloniaExtensions.Axaml.Converters.Switch;

internal class SwitchMultiValueConverter : IMultiValueConverter
{
    private readonly SwitchBindingExtension _switchExtension;

    public SwitchMultiValueConverter(SwitchBindingExtension switchExtension) => _switchExtension = switchExtension;

    public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
    {
        var currentOption = values[_switchExtension.ToIndex];
        if (currentOption == AvaloniaProperty.UnsetValue) return BindingOperations.DoNothing;

        var @case = _switchExtension.Cases.FirstOrDefault(item => Equals(currentOption, item.Label)) ??
                    _switchExtension.Cases.FirstOrDefault(item => Equals(Constants.DefaultLabel, item.Label));

        if (@case == null) return null;

        var index = @case.Index;
        return index == Constants.InvalidIndex ? @case.Value : values[index];
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
