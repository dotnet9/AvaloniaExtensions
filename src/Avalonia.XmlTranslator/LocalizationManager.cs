using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Avalonia.XmlTranslator;

public class LocalizationManager
{
    private static LocalizationManager _instance;
    public static LocalizationManager Instance => _instance ??= new LocalizationManager();

    private List<LocalizationLanguage> _languages = new List<LocalizationLanguage>();
    private LocalizationLanguage _currentLanguage;

    // 加载指定目录下的所有语言文件（XML格式）
    public void LoadLanguages(string languagesDirectory)
    {
        if (!Directory.Exists(languagesDirectory))
        {
            return;
        }

        var xmlFiles = Directory.GetFiles(languagesDirectory, "*.xml");
        foreach (var xmlFile in xmlFiles)
        {
            var xmlDoc = XDocument.Load(xmlFile);
            var root = xmlDoc.Element("Localization");
            if (root == null) continue;

            var language = new LocalizationLanguage
            {
                Language = root.Attribute("language")?.Value,
                Description = root.Attribute("description")?.Value,
                CultureName = root.Attribute("cultureName")?.Value
            };

            var elements = root.Elements().ToList();
            foreach (var element in elements)
            {
                language.Entries.Add(new LocalizationEntry { Key = element.Name.LocalName, Value = element.Value });
            }

            _languages.Add(language);
        }
    }

    // 切换当前使用的语言
    public void SetCurrentLanguage(string language)
    {
        _currentLanguage = _languages.FirstOrDefault(l => l.Language == language);
    }

    // 根据键获取当前语言对应的翻译文字
    public string GetTranslation(string key)
    {
        if (_currentLanguage == null) return key;

        var entry = _currentLanguage.Entries.FirstOrDefault(e => e.Key == key);
        return entry?.Value ?? key;
    }

    // 获取支持的所有语言列表
    public List<string> GetSupportedLanguages()
    {
        return _languages.Select(l => l.Language).ToList();
    }
}
