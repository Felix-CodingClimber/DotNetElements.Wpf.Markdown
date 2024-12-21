using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Renderers;

internal sealed class ThematicBreakRenderer : DocumentRenderer<ThematicBreakBlock>
{
    protected override void Write(DocumentMarkdownWriter renderer, ThematicBreakBlock obj)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        MdThematicBreak thematicBreak = new(renderer.Theme);

        renderer.WriteBlock(thematicBreak);
    }
}
