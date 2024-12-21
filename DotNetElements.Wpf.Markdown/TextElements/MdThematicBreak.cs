using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdThematicBreak : TextElementBase
{
    public override TextElement TextElement => paragraph;

    private readonly Paragraph paragraph;

    public MdThematicBreak(MarkdownThemes theme)
    {
        paragraph = new Paragraph();

        InlineUIContainer inlineUIContainer = new();
        Line line = new Line
        {
            Stretch = Stretch.Fill,
            Stroke = theme.ThematicBreakLineBrush,
            X2 = 1,
            StrokeThickness = theme.ThematicBreakLineThickness,
            Margin = theme.ThematicBreakMargin
        };

        inlineUIContainer.Child = line;
        paragraph.Inlines.Add(inlineUIContainer);
    }
}
