using System.Text.RegularExpressions;

namespace DotNetElements.Wpf.Markdown;

internal static class Extensions
{
    public static Uri GetUri(string? url, string? baseUrl)
    {
        string validUrl = RemoveImageSize(url);

#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        if (Uri.TryCreate(validUrl, UriKind.Absolute, out Uri result))
        {
            // The url is already absolute
            return result;
        }
        else if (!string.IsNullOrWhiteSpace(baseUrl))
        {
            // The url is relative, so append the base
            // Trim any trailing "/" from the base and any leading "/" from the url
            baseUrl = baseUrl?.TrimEnd('/');
            validUrl = validUrl.TrimStart('/');

            // Return the base and the url separated by a single "/"
            return new Uri(baseUrl + "/" + validUrl);
        }
        else
        {
            // The url is relative to the file system
            // Add ms-appx
            validUrl = validUrl.TrimStart('/');

            return new Uri("ms-appx:///" + validUrl);
        }
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
    }

    public static string RemoveImageSize(string? url)
    {
        if (string.IsNullOrEmpty(url))
            throw new ArgumentException("URL must not be null or empty", nameof(url));

        // Create a regex pattern to match the URL with width and height
        string pattern = @"([^)\s]+)\s*=\s*\d+x\d+\s*";

        // Replace the matched URL with the URL only
        string result = Regex.Replace(url, pattern, "$1");

        return result;
    }

    /// <summary>
    /// Convert from System.Drawing.Imaging.PixelFormat to System.Windows.Media.PixelFormat
    /// </summary>
    /// <param name="pixelFormat"></param>
    /// <exception cref="NotSupportedException">Conversion is not available</exception>
    /// <returns></returns>
    public static System.Windows.Media.PixelFormat ToMediaFormat(this System.Drawing.Imaging.PixelFormat pixelFormat)
    {
        return pixelFormat switch
        {
            System.Drawing.Imaging.PixelFormat.Format16bppGrayScale => System.Windows.Media.PixelFormats.Gray16,
            System.Drawing.Imaging.PixelFormat.Format16bppRgb555 => System.Windows.Media.PixelFormats.Bgr555,
            System.Drawing.Imaging.PixelFormat.Format16bppRgb565 => System.Windows.Media.PixelFormats.Bgr565,
            System.Drawing.Imaging.PixelFormat.Indexed => System.Windows.Media.PixelFormats.Bgr101010,
            System.Drawing.Imaging.PixelFormat.Format1bppIndexed => System.Windows.Media.PixelFormats.Indexed1,
            System.Drawing.Imaging.PixelFormat.Format4bppIndexed => System.Windows.Media.PixelFormats.Indexed4,
            System.Drawing.Imaging.PixelFormat.Format8bppIndexed => System.Windows.Media.PixelFormats.Indexed8,
            System.Drawing.Imaging.PixelFormat.Format24bppRgb => System.Windows.Media.PixelFormats.Bgr24,
            System.Drawing.Imaging.PixelFormat.Format32bppArgb => System.Windows.Media.PixelFormats.Bgra32,
            System.Drawing.Imaging.PixelFormat.Format32bppPArgb => System.Windows.Media.PixelFormats.Pbgra32,
            System.Drawing.Imaging.PixelFormat.Format32bppRgb => System.Windows.Media.PixelFormats.Bgr32,
            System.Drawing.Imaging.PixelFormat.Format48bppRgb => System.Windows.Media.PixelFormats.Rgb48,
            System.Drawing.Imaging.PixelFormat.Format64bppArgb => System.Windows.Media.PixelFormats.Prgba64,
            System.Drawing.Imaging.PixelFormat.Undefined => System.Windows.Media.PixelFormats.Default,
            _ => throw new NotSupportedException("Conversion not supported with " + pixelFormat.ToString()),
        };
    }
}
