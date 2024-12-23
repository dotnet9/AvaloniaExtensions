using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using AvaloniaXmlTranslator.Models;

namespace AvaloniaXmlTranslator;

public class I18nManager : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public Dictionary<string, LocalizationLanguage> Resources { get; } = new();
    public static I18nManager Instance { get; } = new();

    // 加载指定目录下的所有语言文件（XML格式）
    private I18nManager()
    {
        LocalizationLanguage ReadLanguage(XElement element)
        {
            return new LocalizationLanguage
            {
                Language = element!.Attribute(Consts.LanguageKey)!.Value,
                Description = element.Attribute("description")!.Value,
                CultureName = element.Attribute("cultureName")!.Value
            };
        }

        var xmlFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml", SearchOption.AllDirectories)
            .Where(file =>
            {
                var doc = XDocument.Load(file);
                var root = doc.Root;
                var language = root?.Attribute(Consts.LanguageKey)?.Value;
                var description = root?.Attribute(Consts.DescriptionKey)?.Value;
                var cultureName = root?.Attribute(Consts.CultureNameKey)?.Value;
                return !string.IsNullOrWhiteSpace(language)
                       && !string.IsNullOrWhiteSpace(description)
                       && !string.IsNullOrWhiteSpace(cultureName);
            }).ToList();

        if (xmlFiles.Any() != true)
        {
            throw new Exception("Please provide the language XML file");
        }

        foreach (var xmlFile in xmlFiles)
        {
            var xmlDoc = XDocument.Load(xmlFile);

            var language = ReadLanguage(xmlDoc.Root!);
            if (!Resources.ContainsKey(language.CultureName))
            {
                Resources[language.CultureName] = language;
            }

            var propertyNodes = xmlDoc.Nodes().OfType<XElement>().DescendantsAndSelf()
                .Where(e => e.Descendants().Any() != true).ToList();
            foreach (var propertyNode in propertyNodes)
            {
                var ancestorsNodeNames = propertyNode.AncestorsAndSelf().Reverse().Select(node => node.Name.LocalName);
                var key = string.Join(".", ancestorsNodeNames);
                Resources[language.CultureName].Languages[key] = propertyNode.Value;
            }
        }
    }


    private CultureInfo _culture;

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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Culture)));
            CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler<EventArgs>? CultureChanged;

    public List<LocalizationLanguage>? GetLanguages() => Resources.Select(kvp => kvp.Value).ToList();

    public string? GetResource(string key, string? cultureName = null)
    {
        var culture = Culture.Name;
        if (!string.IsNullOrWhiteSpace(cultureName))
        {
            culture = cultureName;
        }

        if (Instance.Resources.TryGetValue(culture, out var currentLanguages)
            && currentLanguages.Languages.TryGetValue(key, out string resource))
        {
            return resource;
        }

        return string.Empty;
    }
}
