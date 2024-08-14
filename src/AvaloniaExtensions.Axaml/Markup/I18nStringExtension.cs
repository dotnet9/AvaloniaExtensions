using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using AvaloniaExtensions.Axaml.Converters;

namespace AvaloniaExtensions.Axaml.Markup;

public class I18nStringExtension : MultiBinding
{
    public I18nStringExtension(object key)
    {
        this.Mode = BindingMode.OneWay;
        this.Converter = new I18nResourceConverter(this);
        this.KeyConverter = new I18nKeyConverter();
        this.ValueConverter = new I18nValueConverter();
        this.Args = new ArgCollection(this);

        var cultureBinding = new Binding { Source = I18nManager.Instance, Path = nameof(I18nManager.Culture) };
        this.Bindings.Add(cultureBinding);

        this.Key = key;
        if (this.Key is not BindingBase keyBinding)
        {
            keyBinding = new Binding { Source = key };
        }

        this.Bindings.Add(keyBinding);
    }

    public I18nStringExtension(object key, params object[] args) : this(key)
    {
        foreach (object arg in args)
        {
            this.Args.Add(arg);
        }
    }

    public object Key { get; }
    public ArgCollection Args { get; }

    [ConstructorArgument(nameof(KeyConverter))]
    public IValueConverter KeyConverter { get; set; }

    [ConstructorArgument(nameof(ValueConverter))]
    public IValueConverter ValueConverter { get; set; }

    public class ArgCollection(I18nStringExtension owner) : Collection<object>
    {
        internal List<(bool IsBinding, int Index)> Indexes { get; } = [];

        protected override void InsertItem(int index, object item)
        {
            if (item is BindingBase binding)
            {
                this.Indexes.Add((true, owner.Bindings.Count));
                owner.Bindings.Add(binding);
            }
            else
            {
                this.Indexes.Add((false, this.Count));
                base.InsertItem(index, item);
            }
        }
    }
}
