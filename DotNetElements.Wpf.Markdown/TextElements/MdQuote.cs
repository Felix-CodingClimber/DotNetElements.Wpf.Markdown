using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdQuote : TextElementWithChilds
{
    public override TextElement TextElement => section;
    public Section Section => section;

    private readonly Section section;

    public MdQuote(MarkdownThemes theme)
    {
        section = new Section
        {
            BorderBrush = theme.QuoteBorderBrush,
            BorderThickness = theme.QuoteBorderThickness,
            Background = theme.QuoteBackground,
            Foreground = theme.QuoteForeground,
            Padding = theme.QuotePadding,
            Margin = theme.QuoteMargin
        };
    }

    public override void AddChild(TextElementBase child)
    {
        TextElement? element = child.TextElement;

        if (element is null)
            return;

        if (element is Block block)
        {
            section.Blocks.Add(block);
        }
        else if (element is Inline inline)
        {
            Paragraph paragraph = new();
            paragraph.Inlines.Add(inline);
            section.Blocks.Add(paragraph);
        }
    }
}
