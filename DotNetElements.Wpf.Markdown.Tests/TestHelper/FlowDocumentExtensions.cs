using System.Text;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;
using System.Xml.Linq;

namespace DotNetElements.Wpf.Markdown.Tests.TestHelper;

internal static class FlowDocumentExtensions
{
    public static string ToPrettyXaml(this FlowDocument document)
    {
        using StringWriter stringWriter = new();
        XamlWriter.Save(document, stringWriter);
        string xaml = stringWriter.ToString();

        StringBuilder stringBuilder = new();
        XElement element = XElement.Parse(xaml);

        XmlWriterSettings settings = new()
        {
            OmitXmlDeclaration = true,
            Indent = true,
            NewLineOnAttributes = true
        };

        using XmlWriter xmlWriter = XmlWriter.Create(stringBuilder, settings);
        element.Save(xmlWriter);

        return stringBuilder.ToString();
    }
}
