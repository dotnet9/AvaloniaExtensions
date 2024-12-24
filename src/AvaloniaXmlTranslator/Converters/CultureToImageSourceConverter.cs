using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace AvaloniaXmlTranslator.Converters;

public class CultureToImageSourceConverter : IValueConverter
{
    private static Dictionary<string, Bitmap> _resources = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return Convert(value as CultureInfo);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) =>
        throw new NotImplementedException();

    internal Bitmap? Convert(CultureInfo? culture)
    {
        return Convert(culture, 0);
    }

    private Bitmap? Convert(CultureInfo culture, int recursionCounter)
    {
        var cultureName = culture?.Name;
        if (string.IsNullOrWhiteSpace(cultureName))
        {
            return default;
        }

        var cultureParts = cultureName.Split('-');
        if (!cultureParts.Any())
            return default;

        var key = cultureParts.Last();
        if (_resources.ContainsKey(key))
        {
            return _resources[key];
        }

        try
        {
            var bitmap = new Bitmap(AssetLoader.Open(new Uri($"avares://AvaloniaXmlTranslator/Assets/Flats/{key}.gif",
                UriKind.RelativeOrAbsolute)));

            _resources[cultureName] = bitmap;


            return _resources[cultureName];
        }
        catch
        {
            return default;
        }
    }
}
