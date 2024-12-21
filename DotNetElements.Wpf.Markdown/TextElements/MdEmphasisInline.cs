using System.Windows;
using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdEmphasisInline : TextElementWithChilds, ICascadeChild
{
    public override TextElement TextElement => span;

    private readonly Span span;
    private Span? subSuperSpan;

    public MdEmphasisInline()
    {
        span = new Span();
    }

    public override void AddChild(TextElementBase child)
    {
        try
        {
            if (child is ICascadeChild cascadeChild)
                cascadeChild.InheritProperties(this);

            InlineCollection inlines = subSuperSpan is not null ? subSuperSpan.Inlines : span.Inlines;

            if (child is MdInlineText inlineText)
            {
                inlines.Add(inlineText.Run);
            }
            else if (child is MdEmphasisInline emphasisInline)
            {
                inlines.Add(emphasisInline.span);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error in {nameof(MdEmphasisInline)}.{nameof(AddChild)}: {ex.Message}");
        }
    }

    public void InheritProperties(TextElementBase parent)
    {
        if (subSuperSpan is null)
            return;

        subSuperSpan.FontFamily = parent.TextElement.FontFamily;
        subSuperSpan.FontWeight = parent.TextElement.FontWeight;
        subSuperSpan.FontStyle = parent.TextElement.FontStyle;
        subSuperSpan.Foreground = parent.TextElement.Foreground;
    }

    public void SetBold()
    {
        span.FontWeight = FontWeights.Bold;
    }

    public void SetItalic()
    {
        span.FontStyle = FontStyles.Italic;
    }

    public void SetStrikeThrough()
    {
        span.TextDecorations = TextDecorations.Strikethrough;
    }

    public void SetSubscript()
    {
        ConstructSubSuperContainer(BaselineAlignment.Subscript);
    }

    public void SetSuperscript()
    {
        ConstructSubSuperContainer(BaselineAlignment.Superscript);
    }

    public void SetInserted()
    {
        span.TextDecorations = TextDecorations.Underline;
    }

    public void SetMarked(MarkdownThemes theme)
    {
        span.Background = theme.HighlightBrush;
    }

    private void ConstructSubSuperContainer(BaselineAlignment baselineAlignment)
    {
        Span nestedSpan = new()
        {
            FontSize = span.FontSize * 0.8,
            BaselineAlignment = baselineAlignment,
        };

        span.Inlines.Add(nestedSpan);
        subSuperSpan = nestedSpan;
    }
}
