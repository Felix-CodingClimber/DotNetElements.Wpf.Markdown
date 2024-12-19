using Markdig.Syntax;
using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdHeading : IAddChild
{
    public TextElement TextElement => paragraph;

    private readonly Paragraph paragraph;

    public MdHeading(HeadingBlock headingBlock, MarkdownThemes theme)
    {
        paragraph = new Paragraph();

        SetProperties(headingBlock.Level, theme);
    }

    public void AddChild(IAddChild child)
    {
        // todo
        if (child.TextElement is not Inline inlineChild)
        {
            System.Diagnostics.Debug.WriteLine($"Invalid heading child {child}"); // todo debug

            return;
        }

        if (child is ICascadeChild cascadeChild)
            cascadeChild.InheritProperties(this);

        paragraph.Inlines.Add(inlineChild);
    }

    private void SetProperties(int level, MarkdownThemes theme)
    {
        paragraph.FontSize = level switch
        {
            1 => theme.H1FontSize,
            2 => theme.H2FontSize,
            3 => theme.H3FontSize,
            4 => theme.H4FontSize,
            5 => theme.H5FontSize,
            _ => theme.H6FontSize,
        };

        paragraph.Foreground = theme.HeadingForeground;

        paragraph.FontWeight = level switch
        {
            1 => theme.H1FontWeight,
            2 => theme.H2FontWeight,
            3 => theme.H3FontWeight,
            4 => theme.H4FontWeight,
            5 => theme.H5FontWeight,
            _ => theme.H6FontWeight,
        };

        paragraph.Margin = level switch
        {
            1 => theme.H1Margin,
            2 => theme.H2Margin,
            3 => theme.H3Margin,
            4 => theme.H4Margin,
            5 => theme.H5Margin,
            _ => theme.H6Margin,
        };
    }
}
