using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using AvaloniaExtensions.Axaml.Demo.Models;
using AvaloniaExtensions.Axaml.Markup;
using ReactiveUI;

namespace AvaloniaExtensions.Axaml.Demo.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        Task.Run(async () =>
        {
            while (true)
            {
                CurrentTime = DateTime.Now;
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        });
    }

    public string Title => "AppName";

    private bool _status;

    public bool Status
    {
        get => _status;
        set => this.RaiseAndSetIfChanged(ref _status, value);
    }

    private DateTime _currentTime;

    public DateTime CurrentTime
    {
        get => _currentTime;
        set => this.RaiseAndSetIfChanged(ref _currentTime, value);
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


    public List<string>? ItemsA { get; }
    public List<string>? ItemsB { get; } = new List<string>() { "First", "Second" };

    public void RaiseChangeStatusCommandHandler()
    {
        Status = !Status;
        StatusKind = Status ? RunStatusKind.Running : RunStatusKind.NotRunning;
        AlarmStatus = (AlarmStatus)Enum.Parse(typeof(AlarmStatus),
            Random.Shared.Next(Enum.GetNames(typeof(AlarmStatus)).Length).ToString());
    }
}
