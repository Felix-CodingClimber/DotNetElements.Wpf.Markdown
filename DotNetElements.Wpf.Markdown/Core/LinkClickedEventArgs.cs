namespace DotNetElements.Wpf.Markdown.Core;

public sealed class LinkClickedEventArgs : EventArgs
{
    public Uri Uri { get; private init; }

    public LinkClickedEventArgs(Uri uri)
    {
        Uri = uri;
    }
}
