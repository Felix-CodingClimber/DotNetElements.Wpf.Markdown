using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdParagraph : TextElementWithChilds
{
	public override TextElement TextElement => paragraph;

	private readonly Paragraph paragraph;

	public MdParagraph()
	{
		paragraph = new Paragraph();
	}

	public override void AddChild(TextElementBase child)
	{
		if (child.TextElement is Inline inlineChild)
		{
			paragraph.Inlines.Add(inlineChild);
		}
		else if (child.TextElement is System.Windows.Documents.Block blockChild)
		{
            // todo check if needed and how to implement in WPF

            System.Diagnostics.Debug.WriteLine($"Not implemented paragraph block child {blockChild}"); // todo debug

            //InlineUIContainer inlineUIContainer = new();
            //var richTextBlock = new RichTextBlock();
            //richTextBlock.TextWrapping = TextWrapping.Wrap;
            //richTextBlock.Blocks.Add(blockChild);
            //inlineUIContainer.Child = richTextBlock;

            //paragraph.Inlines.Add(inlineUIContainer);
        }
	}
}
