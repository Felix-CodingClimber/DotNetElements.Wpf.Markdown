using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Renderers;

internal sealed class CodeBlockRenderer : DocumentRenderer<CodeBlock>
{
	protected override void Write(DocumentMarkdownWriter renderer, CodeBlock obj)
	{
		ArgumentNullException.ThrowIfNull(renderer);
		ArgumentNullException.ThrowIfNull(obj);

		MdCodeBlock codeBlock = new(obj, renderer.Config);

        // todo is this really needed?
        // Check for all renderers
        // Maybe we can introduce a new interface IMdElement and IAddChild
        // Only elements that have IAddChild need to be pushed and popped (Check this!)
        renderer.Push(codeBlock);
		renderer.Pop();
	}
}
