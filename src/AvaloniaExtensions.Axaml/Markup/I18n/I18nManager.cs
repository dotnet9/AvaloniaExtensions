using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;

// ReSharper disable once CheckNamespace
namespace AvaloniaExtensions.Axaml.Markup;

public class I18nManager : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;
    private readonly Dictionary<Type, ResourceManager> _resourceManagers;
    private CultureInfo _culture;

    private I18nManager()
    {
        _resourceManagers = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly =>
                assembly.GetTypes()
                    .Where(type => string.Compare(type?.FullName, "i18n", StringComparison.OrdinalIgnoreCase) != 0)
                    .ToDictionary(
                        type => type,
                        type => type.GetProperty(nameof(ResourceManager), BindingFlags.Public | BindingFlags.Static)
                            ?.GetValue(null, null) as ResourceManager)
            )
            .Where(pair => pair.Value != null)
            .ToDictionary(pair => pair.Key, pair => pair.Value!);
        Resources = new Dictionary<string, object>();
        _culture = CultureInfo.InvariantCulture;
    }

    public static I18nManager Instance { get; } = new I18nManager();

    public CultureInfo Culture
    {
        get => _culture;
        set
        {
            if (Equals(_culture, value))
            {
                return;
            }

            _culture = value;
            Thread.CurrentThread.CurrentCulture = value;
            Thread.CurrentThread.CurrentUICulture = value;
            Sync();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Culture)));
            CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public Dictionary<string, object> Resources { get; }

    public event EventHandler<EventArgs>? CultureChanged;

    private void Sync()
    {
        IEnumerable<DictionaryEntry> GetResources(ResourceManager resourceManager)
        {
            var baseEntries = resourceManager.GetResourceSet(CultureInfo.InvariantCulture, true, true)
                ?.OfType<DictionaryEntry>();
            var cultureEntries = resourceManager.GetResourceSet(_culture, true, true)?.OfType<DictionaryEntry>();
            if (cultureEntries == null || baseEntries == null)
            {
                yield break;
            }

            foreach (var entry in cultureEntries
                         .Concat(baseEntries)
                         .GroupBy(entry => entry.Key)
                         .Select(entries => entries.First()))
            {
                yield return entry;
            }
        }

        Resources.Clear();
        foreach (var pair in _resourceManagers)
        {
            pair.Key.GetProperty("Culture", BindingFlags.Public | BindingFlags.Static)?.SetValue(null, _culture);
            foreach (var entry in GetResources(pair.Value))
            {
                Resources[entry.Key.ToString()] = entry.Value;
            }
        }
    }


    public static T? GetResource<T>(string key)
    {
        if (Instance.Resources.TryGetValue(key, out var resource) && resource is T result)
        {
            return result;
        }

        return default;
    }

    public static object? GetObject(string key)
    {
        return GetResource<object>(key);
    }

    public static string? GetString(string key)
    {
        return GetResource<string>(key);
    }
}
