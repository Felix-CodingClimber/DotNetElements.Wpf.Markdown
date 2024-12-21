using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdInlineText : TextElementBase
{
    public override TextElement TextElement => run;
    public Run Run => run;

    private readonly Run run;

    public MdInlineText(string text)
    {
        run = new Run()
        {
            Text = text
        };
    }
}
