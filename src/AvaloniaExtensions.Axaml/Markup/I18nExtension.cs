using System;
using Avalonia.Markup.Xaml;

namespace AvaloniaExtensions.Axaml.Markup;

public class I18nExtension(object key) : MarkupExtension
{
    public override object ProvideValue(IServiceProvider serviceProvider) => new I18nStringExtension(key);
}
