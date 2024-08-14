using System.Globalization;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaExtensions.Axaml.Demo.ViewModels;
using AvaloniaExtensions.Axaml.Demo.Views;
using AvaloniaExtensions.Axaml.Markup;

namespace AvaloniaExtensions.Axaml.Demo
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
            I18nManager.Instance.Culture = new CultureInfo("zh-CN");
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow { DataContext = new MainWindowViewModel(), };
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
