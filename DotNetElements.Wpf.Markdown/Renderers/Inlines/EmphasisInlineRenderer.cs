using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax.Inlines;

namespace DotNetElements.Wpf.Markdown.Renderers.Inlines;

internal sealed class EmphasisInlineRenderer : DocumentRenderer<EmphasisInline>
{
    protected override void Write(DocumentMarkdownWriter renderer, EmphasisInline obj)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        MdEmphasisInline? span = null;

        switch (obj.DelimiterChar)
        {
            case '*':
            case '_':
            {
                span = new MdEmphasisInline();

                if (obj.DelimiterCount == 2)
                    span.SetBold();
                else
                    span.SetItalic();

                break;
            }
            case '~':
            {
                span = new MdEmphasisInline();

                if (obj.DelimiterCount == 2)
                    span.SetStrikeThrough();
                else
                    span.SetSubscript();

                break;
            }
            case '^':
            {
                span = new MdEmphasisInline();

                span.SetSuperscript();

                break;
            }
            case '+':
            {
                span = new MdEmphasisInline();
                span.SetInserted();
                break;
            }
            case '=':
            {
                span = new MdEmphasisInline();
                span.SetMarked(renderer.Config.Themes);
                break;
            }
        }

        if (span is not null)
        {
            renderer.Push(span);
            renderer.WriteChildren(obj);
            renderer.Pop();
        }
        else
        {
            renderer.WriteChildren(obj);
        }
    }
}
