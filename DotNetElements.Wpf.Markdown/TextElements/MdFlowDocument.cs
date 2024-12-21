using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdFlowDocument : TextElementWithChilds
{
    public override TextElement TextElement => throw new InvalidOperationException();

    public FlowDocument Document { get; private set; } = new FlowDocument();

    public override void AddChild(TextElementBase child)
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
