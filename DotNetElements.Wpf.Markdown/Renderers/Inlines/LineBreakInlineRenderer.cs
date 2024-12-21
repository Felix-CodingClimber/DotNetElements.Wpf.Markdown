using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax.Inlines;

namespace DotNetElements.Wpf.Markdown.Renderers.Inlines;

internal sealed class LineBreakInlineRenderer : DocumentRenderer<LineBreakInline>
{
    protected override void Write(DocumentMarkdownWriter renderer, LineBreakInline obj)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        if (obj.IsHard)
        {
            renderer.WriteInline(new MdLineBreak());
        }
        else
        {
            // Soft line break
            renderer.WriteText(" ");
        }
    }
}
