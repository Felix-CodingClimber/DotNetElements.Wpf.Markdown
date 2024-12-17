using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Renderers;

internal sealed class ParagraphRenderer : DocumentRenderer<ParagraphBlock>
{
	protected override void Write(DocumentMarkdownWriter renderer, ParagraphBlock obj)
	{
		ArgumentNullException.ThrowIfNull(renderer);
		ArgumentNullException.ThrowIfNull(obj);

		MdParagraph paragraph = new();

		renderer.Push(paragraph);
		renderer.WriteLeafInline(obj);
		renderer.Pop();
	}
}
