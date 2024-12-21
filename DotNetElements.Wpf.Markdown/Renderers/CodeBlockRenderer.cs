using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Renderers;

internal sealed class CodeBlockRenderer : DocumentRenderer<CodeBlock>
{
    protected override void Write(DocumentMarkdownWriter renderer, CodeBlock obj)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        MdCodeBlock codeBlock = new(obj, renderer.Theme);

        renderer.WriteBlock(codeBlock);
    }
}
