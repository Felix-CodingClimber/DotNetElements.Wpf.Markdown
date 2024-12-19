using System.Windows.Media.Imaging;

namespace DotNetElements.Wpf.Markdown.Core;

public interface IImageProvider
{
    Task<BitmapSource> GetImageAsync(string url, MarkdownConfig config);
    bool ShouldUseThisProvider(string url);
}
