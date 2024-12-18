using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;

namespace DotNetElements.Wpf.Markdown;

internal sealed class DefaultImageProvider : IImageProvider
{
    public async Task<BitmapSource> GetImageAsync(string url, MarkdownConfig config)
    {
        using var stream = new MemoryStream(await File.ReadAllBytesAsync(url));
        using Bitmap bitmap = new Bitmap(stream);

        BitmapSource imageSource = BitmapToBitmapSource(bitmap);
        imageSource.Freeze();

        return imageSource;
    }

    public bool ShouldUseThisProvider(string url)
    {
        return true;
    }

    // https://stackoverflow.com/a/30729291
    private static BitmapSource BitmapToBitmapSource(Bitmap bmp)
    {
        BitmapData bitmapData = bmp.LockBits(
               new Rectangle(0, 0, bmp.Width, bmp.Height),
               ImageLockMode.ReadOnly, bmp.PixelFormat);

        BitmapSource bitmapSource = BitmapSource.Create(
            bitmapData.Width, bitmapData.Height,
            bmp.HorizontalResolution, bmp.VerticalResolution,
            bmp.PixelFormat.ToMediaFormat(), null,
            bitmapData.Scan0, bitmapData.Stride * bitmapData.Height, bitmapData.Stride);

        bmp.UnlockBits(bitmapData);

        return bitmapSource;
    }
}
