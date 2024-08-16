using System;
using Avalonia.Data;
using AvaloniaExtensions.Axaml.Converters;

// ReSharper disable once CheckNamespace
namespace AvaloniaExtensions.Axaml.Markup;

public class IfBindingExtension : MultiBindingExtensionBase
{
    public const int InvalidIndex = -1;

    public int ConditionIndex = InvalidIndex;
    public int TrueIndex = InvalidIndex;
    public int FalseIndex = InvalidIndex;

    public object? TrueContent;
    public object? FalseContent;

    public object? Condition
    {
        set => SetProperty(value, ref ConditionIndex, out _);
    }

    public object? True
    {
        set => SetProperty(value, ref TrueIndex, out TrueContent);
    }

    public object? False
    {
        set => SetProperty(value, ref FalseIndex, out FalseContent);
    }

    public IfBindingExtension() => Converter = new IfMultiValueConverter(this);

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

    private void SetProperty<T>(T value, ref int index, out T? storage)
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
}
