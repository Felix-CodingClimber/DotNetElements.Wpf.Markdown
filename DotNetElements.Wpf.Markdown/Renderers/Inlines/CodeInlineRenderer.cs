using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax.Inlines;

namespace DotNetElements.Wpf.Markdown.Renderers.Inlines;

internal sealed class CodeInlineRenderer : DocumentRenderer<CodeInline>
{
    protected override void Write(DocumentMarkdownWriter renderer, CodeInline obj)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        renderer.WriteInline(new MdInlineCode(obj, renderer.Theme));
    }
}
