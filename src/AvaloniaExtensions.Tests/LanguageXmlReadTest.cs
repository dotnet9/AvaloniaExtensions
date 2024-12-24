using System.Globalization;
using System.Xml.Linq;

namespace AvaloniaExtensions.Tests
{
    [TestClass]
    public sealed class LanguageXmlReadTest
    {
        [TestMethod]
        public void ReadXmlTest()
        {
            var xmlPath = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.xml").FirstOrDefault();
            Assert.IsTrue(File.Exists(xmlPath));

            var doc = XDocument.Load(xmlPath);
            Dictionary<string, Dictionary<string, List<string>>> languageKeys = new();

            var classNodes = doc
                .Nodes()
                .OfType<XElement>().DescendantsAndSelf()
                .Where(e => e.Descendants().Count() == 0)
                .Select(e => e.Parent)
                .Distinct()
                .ToList();
            foreach (var classNode in classNodes)
            {
                var namespaceSegments = classNode.Ancestors().Reverse().Select(node => node.Name.LocalName);
                var namespaceName = string.Join(".", namespaceSegments);
                if (!languageKeys.ContainsKey(namespaceName))
                {
                    languageKeys[namespaceName] = new Dictionary<string, List<string>>();
                }

                GenerateClasses(classNode, namespaceName, languageKeys);
            }

            Assert.IsTrue(languageKeys.Count > 0);
        }


        [TestMethod]
        public void ReadCulturesTest()
        {
            var _availableCultures = CultureInfo.GetCultures(CultureTypes.AllCultures)
                .Where(culture => !CultureInfo.InvariantCulture.Equals(culture))
                //.Except(existingLanguages)
                .OrderBy(culture => culture.DisplayName)
                .ToArray();

            Assert.IsTrue(_availableCultures.Length > 0);
        }

        private void GenerateClasses(XElement element, string namespaceName,
            Dictionary<string, Dictionary<string, List<string>>> languageKeys)
        {
            string className = element.Name.LocalName;
            languageKeys[namespaceName][className] = new();

            var fieldNodes = element.Elements();
            foreach (var fieldNode in fieldNodes)
            {
                var filedName = fieldNode.Name.LocalName;
                languageKeys[namespaceName][className].Add(filedName);
            }
        }
    }
}
