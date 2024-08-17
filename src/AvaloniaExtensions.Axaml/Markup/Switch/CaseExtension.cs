using System;
using Avalonia.Markup.Xaml;

// ReSharper disable once CheckNamespace
namespace AvaloniaExtensions.Axaml.Markup;

public class CaseExtension : MarkupExtension
{
    internal int Index { get; set; } = Constants.InvalidIndex;

    public object Label { get; set; } = Constants.DefaultLabel;

    public object? Value { get; set; }

    public CaseExtension()
    {
    }

    public CaseExtension(object value)
    {
        Value = value;
    }

    public CaseExtension(object option, object value)
    {
        Label = option;
        Value = value;
    }

    public override object ProvideValue(IServiceProvider serviceProvider) => this;
}
