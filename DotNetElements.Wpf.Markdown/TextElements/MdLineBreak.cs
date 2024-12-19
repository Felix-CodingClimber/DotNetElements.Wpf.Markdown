using System.Windows.Documents;
using DotNetElements.Wpf.Markdown.Core;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdLineBreak : IAddChild
{
	public TextElement TextElement => lineBreak;

	private readonly LineBreak lineBreak;

	public MdLineBreak()
	{
		lineBreak = new LineBreak();
	}

	// Not used here
	public void AddChild(IAddChild child) => throw new InvalidOperationException();
}
