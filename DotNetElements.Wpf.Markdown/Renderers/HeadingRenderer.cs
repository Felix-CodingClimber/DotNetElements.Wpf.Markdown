using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Renderers;

internal sealed class HeadingRenderer : DocumentRenderer<HeadingBlock>
{
	protected override void Write(DocumentMarkdownWriter renderer, HeadingBlock obj)
	{
		ArgumentNullException.ThrowIfNull(renderer);
		ArgumentNullException.ThrowIfNull(obj);

		MdHeading heading = new(obj, renderer.Theme);

		renderer.Push(heading);
		renderer.WriteLeafInline(obj);
		renderer.Pop();
	}
}
