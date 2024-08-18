using System;
using Avalonia.Data;
using AvaloniaExtensions.Axaml.Converters;

// ReSharper disable once CheckNamespace
namespace AvaloniaExtensions.Axaml.Markup;

public class SwitchBinding : MultiBindingExtensionBase
{
    public SwitchBinding()
    {
        Mode = BindingMode.OneWay;
        Converter = new SwitchConverter(this);
        Cases = new(this);
    }

    public SwitchBinding(object to, CaseCollection cases) : this()
    {
        To = to;
        Cases = cases;
    }

    private object? _to;
    public int ToIndex = Constants.InvalidIndex;

    public object? To
    {
        get => _to;
        set => SetProperty(value, ref ToIndex, out _to);
    }

    public CaseCollection Cases { get; }

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
