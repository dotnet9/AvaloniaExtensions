using System;
using System.Globalization;
using AvaloniaExtensions.Axaml.Demo.Models;
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

    private AlarmStatus _alarmStatus;

    public AlarmStatus AlarmStatus
    {
        get => _alarmStatus;
        set => this.RaiseAndSetIfChanged(ref _alarmStatus, value);
    }

    private RunStatusKind _statusKind;

    public RunStatusKind StatusKind
    {
        get => _statusKind;
        set => this.RaiseAndSetIfChanged(ref _statusKind, value);
    }

    public void RaiseChangeLanguageHandler(string language)
    {
        I18nManager.Instance.Culture = new CultureInfo(language);
    }

    public void RaiseChangeStatusCommandHandler()
    {
        Status = !Status;
        StatusKind = Status ? RunStatusKind.Running : RunStatusKind.NotRunning;
        AlarmStatus = (AlarmStatus)Enum.Parse(typeof(AlarmStatus),
            Random.Shared.Next(Enum.GetNames(typeof(AlarmStatus)).Length).ToString());
    }
}
