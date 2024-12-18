using System.Windows;
using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdEmphasisInline : IAddChild
{
    public TextElement TextElement => span;

    private readonly Span span;

    private bool isBold;
    private bool isItalic;
    private bool isStrikeThrough;

    public MdEmphasisInline()
    {
        span = new Span();
    }

    public void AddChild(IAddChild child)
    {
        try
        {
            if (child is MdInlineText inlineText)
            {
                span.Inlines.Add(inlineText.Run);
            }
            else if (child is MdEmphasisInline emphasisInline)
            {
                if (emphasisInline.isBold)
                    SetBold();

                if (emphasisInline.isItalic)
                    SetItalic();

                if (emphasisInline.isStrikeThrough)
                    SetStrikeThrough();

                span.Inlines.Add(emphasisInline.span);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Error in {nameof(MdEmphasisInline)}.{nameof(AddChild)}: {ex.Message}");
        }
    }

    public void SetBold()
    {
        span.FontWeight = FontWeights.Bold;

        isBold = true;
    }

    public void SetItalic()
    {
        span.FontStyle = FontStyles.Italic;
        isItalic = true;
    }

    public void SetStrikeThrough()
    {
        span.TextDecorations = TextDecorations.Strikethrough;
        isStrikeThrough = true;
    }

    // todo not working properly
    public void SetSubscript()
    {
        span.SetValue(Typography.VariantsProperty, FontVariants.Subscript);
    }

    // todo not working properly
    public void SetSuperscript()
    {
        span.SetValue(Typography.VariantsProperty, FontVariants.Superscript);
    }

    public void SetInserted()
    {
        span.TextDecorations = TextDecorations.Underline;
    }

    public void SetMarked(MarkdownThemes theme)
    {
        span.Background = theme.HighlightBrush;
    }
}
