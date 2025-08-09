# AvaloniaExtensions

English | [简体中文](README.zh-CN.md)

A syntactic sugar library that brings convenience to Avalonia UI development, aiming to enhance development efficiency and experience by providing additional functionality and simplifying common operations.

## Install

```bash
Install-Package AvaloniaExtensions.Axaml
```

## I18n

https://github.com/dotnet9/Lang.Avalonia

## If

Using the Conditional expression in AXAML.

```axaml
<TextBlock Text="{markup:If Condition={Binding Status}, True=Running, False=NotRunning}" />
<TextBlock Text="{markup:If {Binding Status}, {markup:I18n {x:Static l:Language.Running}}, {markup:I18n {x:Static l:Language.NotRunning}}}" />
```

## Converter Extensions

### IfConditionConverter

```axaml
<markup:IfConditionConverter x:Key="StatusConditionConverter">
            <markup:IfConditionConverter.True>
                <TextBlock Foreground="Green" Text="{markup:I18n {x:Static l:Language.Running}}" />
            </markup:IfConditionConverter.True>
            <markup:IfConditionConverter.False>
                <TextBlock Foreground="DarkOrange" Text="{markup:I18n {x:Static l:Language.NotRunning}}" />
            </markup:IfConditionConverter.False>
        </markup:IfConditionConverter>
```

```axaml
<ContentControl Content="{Binding Status, Converter={StaticResource StatusConditionConverter}}" />
```

## Thanks

Taken from open source, contributed to open source. The development of this library was inspired and aided by the following open-source projects or articles:

- WpfExtensions: [https://github.com/DingpingZhang/WpfExtensions](https://github.com/DingpingZhang/WpfExtensions)
- I18N：[https://github.com/Antelcat/I18N](https://github.com/Antelcat/I18N)
- WPF或Avalonia使用tt模板和resx文件实现国际化：[https://blog.csdn.net/eyupaopao/article/details/136638194](https://blog.csdn.net/eyupaopao/article/details/136638194)

Special thanks to the contributors of these projects. Their efforts have provided valuable references and assistance for the development of AvaloniaExtensions.
