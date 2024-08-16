using System.Globalization;
using AvaloniaExtensions.Axaml.Markup;
using ReactiveUI;

namespace AvaloniaExtensions.Axaml.Demo.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Title => "AppName";

    private bool _status;

    public bool Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }

    public void RaiseChangeLanguageHandler(string language)
    {
        I18nManager.Instance.Culture = new CultureInfo(language);
    }

    public void RaiseChangeStatusCommandHandler()
    {
        Status = !Status;
    }
}
