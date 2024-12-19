using System.Windows.Documents;
using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax.Inlines;

namespace DotNetElements.Wpf.Markdown.Renderers.Inlines;

internal sealed class LinkInlineRenderer : DocumentRenderer<LinkInline>
{
    protected override void Write(DocumentMarkdownWriter renderer, LinkInline obj)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        string? url = obj.GetDynamicUrl is not null ? obj.GetDynamicUrl() ?? obj.Url : obj.Url;

        if (!Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            url = "#"; // todo log warning

        if (obj.IsImage)
        {
            MdImage image = new(url, renderer.Config, renderer.Theme);
            renderer.WriteInline(image);
        }
        else
        {
            if (obj.FirstChild is LinkInline linkInlineChild && linkInlineChild.IsImage)
                throw new NotSupportedException("Image link inside a link is not supported.");

            MdInlineHyperlink hyperlink = new(obj, renderer.Config.BaseUrl);

            hyperlink.ClickEvent += (sender, e) =>
            {
                renderer.OnLinkClicked(((Hyperlink)sender).NavigateUri);
            };

            renderer.Push(hyperlink);

            renderer.WriteChildren(obj);
            renderer.Pop();
        }
    }
}
