using System.Windows.Documents;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdQuote : IAddChild
{
    public TextElement TextElement => section;

    private readonly Section section;

    public MdQuote(QuoteBlock quoteBlock, MarkdownConfig config)
    {
        section = new Section();

        section.BorderBrush = config.Themes.QuoteBorderBrush;
        section.BorderThickness = config.Themes.QuoteBorderThickness;
        section.Background = config.Themes.QuoteBackground;
        section.Foreground = config.Themes.QuoteForeground;
        section.Padding = config.Themes.QuotePadding;
    }

    // todo fix top and bottom margin/ padding (caused by paragraph margin)
    // todo fix multiline quote rendered in a single line!
    public void AddChild(IAddChild child)
    {
        TextElement? element = child.TextElement;

        if (element is null)
            return;

        if (element is System.Windows.Documents.Block block)
        {
            section.Blocks.Add(block);
        }
        else if (element is System.Windows.Documents.Inline inline)
        {
            Paragraph paragraph = new();
            paragraph.Inlines.Add(inline);
            section.Blocks.Add(paragraph);
        }
    }
}
