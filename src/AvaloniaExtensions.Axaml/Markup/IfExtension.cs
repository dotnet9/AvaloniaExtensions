using System;
using System.Collections.Generic;
using System.Globalization;
using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using AvaloniaExtensions.Axaml.ExtensionMethods;

namespace AvaloniaExtensions.Axaml.Markup;

public class IfExtension : MarkupExtension
{
    public object? Condition { get; set; }
    public object? True { get; set; }
    public object? False { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        new IfBindingExtension(Condition, True, False);
}

public class IfBindingExtension : MultiBindingExtensionBase
{
    private const int InvalidIndex = -1;

    private int _conditionIndex = InvalidIndex;
    private int _trueIndex = InvalidIndex;
    private int _falseIndex = InvalidIndex;

    private object? _true;
    private object? _false;

    [ConstructorArgument(nameof(Condition))]
    public object? Condition
    {
        set => SetProperty(value, ref _conditionIndex, out _);
    }

    [ConstructorArgument(nameof(True))]
    public object? True
    {
        set => SetProperty(value, ref _trueIndex, out _true);
    }

    [ConstructorArgument(nameof(False))]
    public object? False
    {
        set => SetProperty(value, ref _falseIndex, out _false);
    }

    public IfBindingExtension() => Converter = new MultiValueConverter(this);

    public IfBindingExtension(object? condition, object? trueValue, object? falseValue)
        : this()
    {
        if (condition is not BindingBase keyBinding)
        {
            keyBinding = new Binding { Source = condition };
        }
        Condition = keyBinding;
        True = trueValue;
        False = falseValue;
    }

    private void SetProperty<T>(T value, ref int index, out T storage)
    {
        if (index != InvalidIndex)
            throw new InvalidOperationException("Cannot reset the value. ");

        if (value is BindingBase binding)
        {
            Bindings.Add(binding);
            index = Bindings.Count - 1;
            storage = default;
        }
        else
        {
            storage = value;
        }
    }


    private class MultiValueConverter(IfBindingExtension ifExtension) : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            var condition = values[ifExtension._conditionIndex];

            if (condition == AvaloniaProperty.UnsetValue) return BindingOperations.DoNothing;

            return condition.CastTo<bool>()
                ? GetValue(ifExtension._trueIndex, ifExtension._true)
                : GetValue(ifExtension._falseIndex, ifExtension._false);

            object GetValue(int index, object storage) => index != InvalidIndex ? values[index] : storage;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
