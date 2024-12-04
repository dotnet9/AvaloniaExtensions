using System;
using System.Text;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Avalonia.XmlTranslator.Demo;

[Generator]
public class LocalizationGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
    }

    public void Execute(GeneratorExecutionContext context)
    {
        string xmlFilePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "I18n", "en.xml");
        XDocument xmlDoc = XDocument.Load(xmlFilePath);

        StringBuilder sourceBuilder = new StringBuilder();

        // 添加命名空间声明
        sourceBuilder.AppendLine($"namespace Localization;");

        foreach (XElement element in xmlDoc.Root.Elements())
        {
            string className = element.Name.LocalName;
            sourceBuilder.AppendLine($"    public partial class {className}");
            sourceBuilder.AppendLine($"    {{");

            foreach (XElement propertyElement in element.Elements())
            {
                string propertyName = propertyElement.Name.LocalName;
                sourceBuilder.AppendLine($"        public string {propertyName} {{ get; set; }}");
            }

            sourceBuilder.AppendLine($"    }}");
        }

        context.AddSource("LocalizationGenerated.cs", SourceText.From(sourceBuilder.ToString(), Encoding.UTF8));
    }
}
