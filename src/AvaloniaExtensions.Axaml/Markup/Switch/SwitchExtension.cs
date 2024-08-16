using System;
using Avalonia.Data;
using Avalonia.Markup.Xaml;

// ReSharper disable once CheckNamespace
namespace AvaloniaExtensions.Axaml.Markup;

public partial class SwitchExtension : MarkupExtension
{
    public SwitchExtension()
    {
    }

    public object? To { get; }
    public CaseCollection? Cases { get; }

    public override object ProvideValue(IServiceProvider serviceProvider) =>
        new SwitchBindingExtension(To, Cases);
}
