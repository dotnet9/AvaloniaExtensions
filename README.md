# AvaloniaExtensions  
  
A syntactic sugar library that brings convenience to Avalonia UI development.

## Install

```bash
Install-Package AvaloniaExtensions.Axaml -Version 0.0.1
```

## I18n

MainWindow8.axaml:

```axaml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="using:AvaloniaExtensions.Axaml.Demo.ViewModels"
        xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
        xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
        xmlns:i18n="https://codewf.com"
        xmlns:l="clr-namespace:AvaloniaExtensions.Axaml.Demo.I18n"
        mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
        x:Class="AvaloniaExtensions.Axaml.Demo.Views.MainWindow"
        x:DataType="vm:MainWindowViewModel"
        Icon="/Assets/avalonia-logo.ico"
        Title="{i18n:I18n {Binding Title}}">

    <Design.DataContext>
        <!-- This only sets the DataContext for the previewer in an IDE,
             to set the actual DataContext for runtime, set the DataContext property in code (look at App.axaml.cs) -->
        <vm:MainWindowViewModel />
    </Design.DataContext>

    <StackPanel>
        <TextBlock HorizontalAlignment="Center"
                   VerticalAlignment="Center">
            <Run Text="{i18n:I18n {Binding Title}}" />
            <Run Text="{i18n:I18n {x:Static l:LanguageKeys.Welcome}}" />
        </TextBlock>
        <StackPanel Orientation="Horizontal">
            <Button Content="English" Command="{Binding RaiseChangeLanguageHandler}" CommandParameter="en" />
            <Button Content="中文简体" Command="{Binding RaiseChangeLanguageHandler}" CommandParameter="zh-CN" />
            <Button Content="中文繁體" Command="{Binding RaiseChangeLanguageHandler}" CommandParameter="zh-Hant" />
            <Button Content="日本語" Command="{Binding RaiseChangeLanguageHandler}" CommandParameter="ja-JP" />
        </StackPanel>
    </StackPanel>

</Window>
```

MainWindowViewwModel:

```csharp
public class MainWindowViewModel : ViewModelBase
{
    public string Title => "AppName";

    public void RaiseChangeLanguageHandler(string language)
    {
        I18nManager.Instance.Culture = new CultureInfo(language);
    }
}
```

```csharp
public static class Language
{
		public static readonly string AppName = "AppName";
		public static readonly string Welcome = "Welcome";
}
```
  
## Acknowledgments  
  
Taking from open source and contributing to open source. The development of this library was referenced from the following open source projects or articles:  
  
- WpfExtensions: [https://github.com/DingpingZhang/WpfExtensions](https://github.com/DingpingZhang/WpfExtensions)
