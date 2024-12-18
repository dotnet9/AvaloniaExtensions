using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AvaloniaXmlTranslator;
using AvaloniaXmlTranslator.Models;
using ReactiveUI;

namespace Avalonia.XmlTranslator.Demo.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public List<LocalizationLanguage> Languages { get; set; }
    public LocalizationLanguage? _selectLanguage;

    public LocalizationLanguage? SelectLanguage
    {
        get => _selectLanguage;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectLanguage, value);
            I18nManager.Instance.Culture = new CultureInfo(value.CultureName);
        }
    }

    public MainWindowViewModel()
    {
        Languages = I18nManager.Instance.Resources.Select(kvp => kvp.Value).ToList();
        SelectLanguage = Languages.FirstOrDefault(l => l.CultureName == I18nManager.Instance.Culture.Name);
    }
}
