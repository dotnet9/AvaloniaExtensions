using System;
using Avalonia.Data;
using AvaloniaExtensions.Axaml.Converters;

// ReSharper disable once CheckNamespace
namespace AvaloniaExtensions.Axaml.Markup;

public class IfBindingExtension : MultiBindingExtensionBase
{
    public int ConditionIndex = Constants.InvalidIndex;
    public int TrueIndex = Constants.InvalidIndex;
    public int FalseIndex = Constants.InvalidIndex;

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
        Condition = condition;
        True = trueValue;
        False = falseValue;
    }

    private void SetProperty<T>(T value, ref int index, out T? storage)
    {
        if (index != Constants.InvalidIndex)
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
