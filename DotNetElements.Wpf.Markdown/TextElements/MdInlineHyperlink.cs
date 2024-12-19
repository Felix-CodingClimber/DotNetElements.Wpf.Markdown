using System.Windows;
using System.Windows.Documents;
using DotNetElements.Wpf.Markdown.Core;
using Markdig.Syntax.Inlines;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdInlineHyperlink : IAddChild
{
    public TextElement TextElement => hyperlink;

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

    public void AddChild(IAddChild child)
    {
        // todo
        if (child.TextElement is not System.Windows.Documents.Inline inlineChild)
        {
            System.Diagnostics.Debug.WriteLine($"Invalid hyperlink child {child}"); // todo debug

            return;
        }

        try
        {
            hyperlink.Inlines.Add(inlineChild);
            // todo add support for click handler
        }
        catch
        {
            // todo handle exception
        }
    }
}
