using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Renderers;

internal sealed class QuoteBlockRenderer : DocumentRenderer<QuoteBlock>
{
	protected override void Write(DocumentMarkdownWriter renderer, QuoteBlock obj)
	{
		ArgumentNullException.ThrowIfNull(renderer);
		ArgumentNullException.ThrowIfNull(obj);

		MdQuote quote = new(obj, renderer.Config);

        renderer.Push(quote);
        renderer.WriteChildren(obj);
        renderer.Pop();
	}
}
