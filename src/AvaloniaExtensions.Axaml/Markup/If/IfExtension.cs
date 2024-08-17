using System;
using Avalonia.Markup.Xaml;

// ReSharper disable once CheckNamespace
namespace AvaloniaExtensions.Axaml.Markup;

public class IfExtension : MarkupExtension
{
    public IfExtension()
    {
    }

    public IfExtension(object condition, object trueValue, object falseValue)
    {
        Condition = condition;
        True = trueValue;
        False = falseValue;
    }

    public object? Condition { get; set; }

    public object? True { get; set; }

    public object? False { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        new IfBinding(Condition, True, False);
}
