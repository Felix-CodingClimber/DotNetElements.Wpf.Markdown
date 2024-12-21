namespace DotNetElements.Wpf.Markdown;

public record MarkdownConfig
{
    public static MarkdownConfig Default => new();

    public string? BaseUrl { get; set; }
    public IImageProvider? ImageProvider { get; set; }
    public string LocalImagePath { get; set; } = "img";
    public bool FeatureEmphasisExtrasSupported { get; set; } = true; // todo use subclass like MarkdownThemes or enum flags for features
    public bool FeatureImageSizeSupported { get; set; } = true; // todo use subclass like MarkdownThemes or enum flags for features
    public bool FeatureTaskListSupported { get; set; } = true; // todo use subclass like MarkdownThemes or enum flags for features
    public bool FeaturePipeTablesSupported { get; set; } = true; // todo use subclass like MarkdownThemes or enum flags for features
    public bool FeatureAlertBlocksSupported { get; set; } = true; // todo use subclass like MarkdownThemes or enum flags for features
    public bool FeatureAutoLinksSupported { get; set; } = true; // todo use subclass like MarkdownThemes or enum flags for features
}
