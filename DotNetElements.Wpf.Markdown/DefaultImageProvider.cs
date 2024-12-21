using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Windows.Media.Imaging;

namespace DotNetElements.Wpf.Markdown;

internal sealed class DefaultImageProvider : IImageProvider
{
    public Task<BitmapSource> GetImageAsync(string url, MarkdownConfig config)
    {
        if (url.StartsWith(config.LocalImagePath))
            return GetLocalImageAsync(url, config);
        else
            return GetWebImageAsync(url, config);
    }

    private async Task<BitmapSource> GetLocalImageAsync(string url, MarkdownConfig config)
    {
        using MemoryStream stream = new(await File.ReadAllBytesAsync(url));
        using Bitmap bitmap = new Bitmap(stream);

        BitmapSource imageSource = BitmapToBitmapSource(bitmap);
        imageSource.Freeze();

        return imageSource;
    }

    private async Task<BitmapSource> GetWebImageAsync(string url, MarkdownConfig config)
    {
        HttpClient client = new();

        HttpResponseMessage response = await client.GetAsync(url);
        string? contentType = response.Content.Headers.ContentType?.MediaType;

        if (contentType == "image/svg+xml")
        {
            // todo
            //var svgString = await response.Content.ReadAsStringAsync();
            //var resImage = await _svgRenderer.SvgToImage(svgString);
            //if (resImage != null)
            //{
            //    _image = resImage;
            //    _container.Child = _image;
            //}

            return null!;
        }
        else
        {
            byte[] data = await response.Content.ReadAsByteArrayAsync();

            using MemoryStream stream = new MemoryStream();
            await stream.WriteAsync(data);
            stream.Seek(0, SeekOrigin.Begin);

            Bitmap bitmap = new Bitmap(stream);

            return BitmapToBitmapSource(bitmap);
        }
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
