using System.Windows.Documents;
using Markdig.Helpers;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdCodeBlock : IAddChild
{
    public TextElement TextElement => paragraph;

    private readonly Paragraph paragraph;

    public MdCodeBlock(CodeBlock codeBlock, MarkdownThemes theme)
    {
        paragraph = new Paragraph
        {
            Background = theme.CodeBlockBackground,
            Foreground = theme.CodeBlockForeground,
            FontSize = theme.CodeBlockFontSize,
            FontWeight = theme.CodeBlockFontWeight,
            Padding = theme.CodeBlockPadding,
            BorderBrush = theme.CodeBlockBorderBrush,
            BorderThickness = theme.CodeBlockBorderThickness
        };

        foreach (StringLine line in codeBlock.Lines.Lines)
        {
            string lineString = line.ToString();

            if (string.IsNullOrWhiteSpace(lineString))
                continue;

            paragraph.Inlines.Add(new Run() { Text = lineString });
            paragraph.Inlines.Add(new LineBreak());
        }

        // Remove last line break
        paragraph.Inlines.Remove(paragraph.Inlines.LastInline);
    }

    // Not used here
    public void AddChild(IAddChild child) => throw new InvalidOperationException();
}
