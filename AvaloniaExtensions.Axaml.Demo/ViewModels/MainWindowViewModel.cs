using System.Globalization;
using AvaloniaExtensions.Axaml.Markup;

namespace AvaloniaExtensions.Axaml.Demo.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Title => "AppName";

    public void RaiseChangeLanguageHandler(string language)
    {
        I18nManager.Instance.Culture = new CultureInfo(language);
    }
}
