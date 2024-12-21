using System.Windows;
using System.Windows.Documents;
using Markdig.Syntax.Inlines;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdInlineHyperlink : TextElementWithChilds
{
    public override TextElement TextElement => hyperlink;

    public event RoutedEventHandler ClickEvent
    {
        add
        {
            hyperlink.Click += value;
        }
        remove
        {
            hyperlink.Click -= value;
        }
    }

    private readonly Hyperlink hyperlink;

    public MdInlineHyperlink(LinkInline linkInline, string? baseUrl)
    {
        string? url = linkInline.GetDynamicUrl is not null ? linkInline.GetDynamicUrl() ?? linkInline.Url : linkInline.Url;

        hyperlink = new Hyperlink()
        {
            NavigateUri = Extensions.GetUri(url, baseUrl),
        };
    }

    public override void AddChild(TextElementBase child)
    {
        if (child.TextElement is not System.Windows.Documents.Inline inlineChild)
            throw new InvalidOperationException($"Invalid hyperlink child {child}");

        try
        {
            hyperlink.Inlines.Add(inlineChild);
        }
        catch
        {
            // todo handle exception
        }
    }
}
