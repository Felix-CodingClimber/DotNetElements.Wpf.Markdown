using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdQuote : IAddChild
{
    public TextElement TextElement => section;
    public Section Section => section;

    private readonly Section section;

    public MdQuote(MarkdownConfig config)
    {
        section = new Section
        {
            BorderBrush = config.Themes.QuoteBorderBrush,
            BorderThickness = config.Themes.QuoteBorderThickness,
            Background = config.Themes.QuoteBackground,
            Foreground = config.Themes.QuoteForeground,
            Padding = config.Themes.QuotePadding,
            Margin = config.Themes.QuoteMargin
        };
    }

    public void AddChild(IAddChild child)
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
