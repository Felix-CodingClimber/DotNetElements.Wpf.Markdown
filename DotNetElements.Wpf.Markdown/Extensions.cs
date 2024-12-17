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
}
