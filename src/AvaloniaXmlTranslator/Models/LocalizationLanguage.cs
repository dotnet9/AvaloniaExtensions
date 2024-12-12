using System.Collections.Generic;

namespace AvaloniaXmlTranslator.Models;

public class LocalizationLanguage
{
    public string Language { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string CultureName { get; set; } = null!;
    public Dictionary<string, string> Languages { get; } = new();
}
