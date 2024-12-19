using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using DotNetElements.Wpf.Markdown.Core;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdThematicBreak : IAddChild
{
	public TextElement TextElement => paragraph;

	private readonly Paragraph paragraph;

	public MdThematicBreak(MarkdownConfig config)
	{
        paragraph = new Paragraph();

        InlineUIContainer inlineUIContainer = new();
        Line line = new Line
        {
            Stretch = Stretch.Fill,
            Stroke = config.Themes.ThematicBreakLineBrush,
            X2 = 1,
            StrokeThickness = config.Themes.ThematicBreakLineThickness,
            Margin = config.Themes.ThematicBreakMargin
        };

        inlineUIContainer.Child = line;
        paragraph.Inlines.Add(inlineUIContainer);
    }

	// Not used here
	public void AddChild(IAddChild child) => throw new InvalidOperationException();
}
