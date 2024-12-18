namespace DotNetElements.Wpf.Markdown;

public record MarkdownConfig
{
	public string? BaseUrl { get; set; }
    public IImageProvider? ImageProvider { get; set; }
	public MarkdownThemes Themes { get; set; } = MarkdownThemes.Default;
    public bool FeatureImageSizeSupported { get; set; } = true; // todo use subclass like MarkdownThemes or enum flags for features
    public bool FeatureTaskListSupported { get; set; } = true; // todo use subclass like MarkdownThemes or enum flags for features
    public bool FeatureTablesSupported { get; set; } = true; // todo use subclass like MarkdownThemes or enum flags for features

    public readonly static MarkdownConfig Default = new();
}
