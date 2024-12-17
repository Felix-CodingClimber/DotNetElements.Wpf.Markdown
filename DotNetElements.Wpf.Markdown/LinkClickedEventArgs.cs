namespace DotNetElements.Wpf.Markdown;

public sealed class LinkClickedEventArgs : EventArgs
{
	public Uri Uri { get; private init; }

	public LinkClickedEventArgs(Uri uri)
	{
		this.Uri = uri;
	}
}
