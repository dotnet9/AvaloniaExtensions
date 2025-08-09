# AvaloniaExtensions

简体中文 | [English](README.md)

为Avalonia UI开发带来便利的语法糖库，旨在通过提供额外的功能和简化常用操作，提升开发效率与体验。

## 安装

```bash
Install-Package AvaloniaExtensions.Axaml
```

## I18n

https://github.com/dotnet9/Lang.Avalonia

## If

在AXAML中使用条件表达式

```axaml
<TextBlock Text="{markup:If Condition={Binding Status}, True=已运行, False=未运行}" />
<TextBlock Text="{markup:If {Binding Status}, {markup:I18n {x:Static l:Language.Running}}, {markup:I18n {x:Static l:Language.NotRunning}}}" />
```

## 扩展转换器

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

## 感谢

取之于开源，献之于开源。本库的开发受到了以下开源项目或文章的启发与帮助：

- WpfExtensions: [https://github.com/DingpingZhang/WpfExtensions](https://github.com/DingpingZhang/WpfExtensions)
- I18N：[https://github.com/Antelcat/I18N](https://github.com/Antelcat/I18N)
- WPF或Avalonia使用tt模板和resx文件实现国际化：[https://blog.csdn.net/eyupaopao/article/details/136638194](https://blog.csdn.net/eyupaopao/article/details/136638194)

特别感谢这些项目的贡献者，他们的努力为AvaloniaExtensions的开发提供了宝贵的参考与帮助。
