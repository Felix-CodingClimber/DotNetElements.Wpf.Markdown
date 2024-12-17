namespace DotNetElements.Wpf.Markdown;

public record MarkdownConfig
{
	public string? BaseUrl { get; set; }
	//public IImageProvider? ImageProvider { get; set; }
	//public ISVGRenderer? SVGRenderer { get; set; }
	public MarkdownThemes Themes { get; set; } = MarkdownThemes.Default;

	public readonly static MarkdownConfig Default = new();
}
