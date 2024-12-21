using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdLineBreak : TextElementBase
{
    public override TextElement TextElement => lineBreak;

    private readonly LineBreak lineBreak;

    public MdLineBreak()
    {
        lineBreak = new LineBreak();
    }
}
