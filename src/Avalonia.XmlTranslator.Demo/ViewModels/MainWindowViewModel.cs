using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AvaloniaXmlTranslator;
using AvaloniaXmlTranslator.Models;
using ReactiveUI;

namespace Avalonia.XmlTranslator.Demo.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Languages = I18nManager.Instance.Resources.Select(kvp => kvp.Value).ToList();
        SelectLanguage = Languages.FirstOrDefault(l => l.CultureName == I18nManager.Instance.Culture.Name);

        AllCultures = new ObservableCollection<CultureInfo>(I18nManager.Instance.GetAvailableCultures());

        var titleCurrentCulture = I18nManager.Instance.GetResource(Localization.Main.MainView.Title);
        var titleZhCN = I18nManager.Instance.GetResource(Localization.Main.MainView.Title, "zh-CN");
        var titleEnUS = I18nManager.Instance.GetResource(Localization.Main.MainView.Title, "en-US");

        Task.Run(async () =>
        {
            while (true)
            {
                CurrentTime = DateTime.Now;
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        });
    }

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

    private DateTime _currentTime;

    public DateTime CurrentTime
    {
        get => _currentTime;
        set => this.RaiseAndSetIfChanged(ref _currentTime, value);
    }

    public ObservableCollection<CultureInfo> AllCultures { get; }
}
