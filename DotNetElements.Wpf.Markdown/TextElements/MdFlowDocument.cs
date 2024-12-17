using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdFlowDocument : IAddChild
{
	// Not used here (Check if  { get; set; } = new Run(); is needed)
	public TextElement TextElement => throw new InvalidOperationException();

	public FlowDocument Document { get; private set; } = new FlowDocument();

	public void AddChild(IAddChild child)
	{
		TextElement? element = child.TextElement;

		if (element is null)
			return;

		if (element is Block block)
		{
			Document.Blocks.Add(block);
		}
		else if (element is Inline inline)
		{
			Paragraph paragraph = new();
			paragraph.Inlines.Add(inline);
			Document.Blocks.Add(paragraph);
		}
	}
}
