using System.Collections.Generic;

namespace Avalonia.XmlTranslator;

public class LocalizationLanguage
{
    public string Language { get; set; }
    public string Description { get; set; }
    public string CultureName { get; set; }
    public List<LocalizationEntry> Entries { get; set; } = new();
}
