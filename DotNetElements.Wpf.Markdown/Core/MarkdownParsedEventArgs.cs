using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Core;

public sealed class MarkdownParsedEventArgs : EventArgs
{
    public MarkdownDocument Document { get; private init; }

    public MarkdownParsedEventArgs(MarkdownDocument document)
    {
        Document = document;
    }
}
