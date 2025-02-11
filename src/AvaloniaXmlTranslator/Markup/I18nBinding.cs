using System.Collections.Generic;
using Avalonia.Data;
using Avalonia.Data.Converters;
using AvaloniaXmlTranslator.Converters;

namespace AvaloniaXmlTranslator.Markup;

public class I18nBinding : MultiBindingExtensionBase
{
    public I18nBinding(object key)
    {
        Mode = BindingMode.OneWay;
        Converter = new I18nConverter(this);
        KeyConverter = new I18nKeyConverter();
        ValueConverter = new I18nValueConverter();
        Args = new ArgCollection(this);

        var cultureBinding = new Binding { Source = I18nManager.Instance, Path = nameof(I18nManager.Culture) };
        Bindings.Add(cultureBinding);

        Key = key;
        if (Key is not BindingBase keyBinding)
        {
            keyBinding = new Binding { Source = key };
        }

        Bindings.Add(keyBinding);
    }

    public I18nBinding(object key, string? cultureName, List<object> args) : this(key)
    {
        CultureName = cultureName;
        if (args is not { Count: > 0 })
        {
            return;
        }

        foreach (object arg in args)
        {
            Args.Add(arg);
        }
    }

    public object Key { get; }

    public string? CultureName { get; set; }

    public ArgCollection Args { get; }

    public IValueConverter KeyConverter { get; set; }

    public IValueConverter ValueConverter { get; set; }
}
