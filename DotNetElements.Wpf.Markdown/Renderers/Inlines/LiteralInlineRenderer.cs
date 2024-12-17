using Markdig.Syntax.Inlines;

namespace DotNetElements.Wpf.Markdown.Renderers.Inlines;

internal sealed class LiteralInlineRenderer : DocumentRenderer<LiteralInline>
{
	protected override void Write(DocumentMarkdownWriter renderer, LiteralInline obj)
	{
		ArgumentNullException.ThrowIfNull(renderer);
		ArgumentNullException.ThrowIfNull(obj);

		if (obj.Content.IsEmpty)
			return;

		renderer.WriteText(ref obj.Content);
	}
}
