using System.Windows.Documents;
using Markdig.Helpers;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdCodeBlock : IAddChild
{
    public TextElement TextElement => paragraph;

    private readonly Paragraph paragraph;

    public MdCodeBlock(CodeBlock codeBlock, MarkdownConfig config)
    {
        paragraph = new Paragraph();

        paragraph.Background = config.Themes.CodeBlockBackground;
        paragraph.Foreground = config.Themes.CodeBlockForeground;
        paragraph.FontSize = config.Themes.CodeBlockFontSize;
        paragraph.FontWeight = config.Themes.CodeBlockFontWeight;
        paragraph.Padding = config.Themes.CodeBlockPadding;
        paragraph.BorderBrush = config.Themes.CodeBlockBorderBrush;
        paragraph.BorderThickness = config.Themes.CodeBlockBorderThickness;

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
