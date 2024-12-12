using System;
using Avalonia.Markup.Xaml;

namespace AvaloniaXmlTranslator.Markup;

public class I18nExtension : MarkupExtension
{
    public I18nExtension(object key)
    {
        Key = key;
    }

    public I18nExtension(object key, params object[] args)
    {
        Key = key;
        Args = args;
    }

    public object Key { get; }
    public object[]? Args { get; }
    public override object ProvideValue(IServiceProvider serviceProvider) => new I18nBinding(Key, Args);
}
