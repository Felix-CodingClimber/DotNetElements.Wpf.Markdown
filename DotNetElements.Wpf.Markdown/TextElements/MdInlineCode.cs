using System.Windows.Documents;
using Markdig.Syntax.Inlines;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdInlineCode : TextElementBase
{
    public override TextElement TextElement => run;

    private readonly Run run;

    public MdInlineCode(CodeInline codeInline, MarkdownThemes theme)
    {
        run = new Run(codeInline.Content)
        {
            Background = theme.InlineCodeBackground,
            Foreground = theme.InlineCodeForeground,
            FontSize = theme.InlineCodeFontSize,
            FontWeight = theme.InlineCodeFontWeight
        };
        //run.Padding = config.InlineCodePadding; // todo not working
    }
}
