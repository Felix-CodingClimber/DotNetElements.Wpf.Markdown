using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdImage : IAddChild
{
    public TextElement TextElement => container;

    private readonly InlineUIContainer container;

    private static readonly DefaultImageProvider defaultImageProvider = new();

    public MdImage(string url, MarkdownConfig config)
    {
        ArgumentNullException.ThrowIfNull(url);

        container = new InlineUIContainer();

        Image image = new Image();
        image.HorizontalAlignment = HorizontalAlignment.Left;

        // Feature: Custom defined image width and height in the format ![Image](img/exampleImg.png=100x100)
        // The height or width can be omitted, but the x is required
        // Example width omitted: ![Image](img/exampleImg.png=x100)
        // Example height omitted: ![Image](img/exampleImg.png=100x)
        string[] urlParts = url.Split('=');

        if (config.FeatureImageSizeSupported && urlParts.Length == 2)
        {
            string[] dimensions = urlParts[1].Split('x');

            if (dimensions.Length == 2)
            {
                url = urlParts[0];

                if (dimensions[0].Length > 0)
                    image.Width = int.Parse(dimensions[0]);

                if (dimensions[1].Length > 0)
                    image.Height = int.Parse(dimensions[1]);
            }
            else
            {
                // todo log warning invalid image size format
            }
        }
        else
        {
            image.MaxWidth = config.Themes.ImageMaxWidth;
            image.MaxHeight = config.Themes.ImageMaxHeight;
        }

        image.Loaded += async (sender, e) =>
        {
            IImageProvider imageProviderToUse = defaultImageProvider;

            if (config.ImageProvider is not null && config.ImageProvider.ShouldUseThisProvider(url))
                imageProviderToUse = config.ImageProvider;

            ((Image)sender).Source = await imageProviderToUse.GetImageAsync(url, config);
        };

        container.Child = image;
    }

    // Not used here
    public void AddChild(IAddChild child) => throw new InvalidOperationException();
}
