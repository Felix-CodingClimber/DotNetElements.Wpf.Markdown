using System.Windows;
using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Extensions.Alerts;

namespace DotNetElements.Wpf.Markdown.Renderers.Extensions;

internal sealed class AlertBlockRenderer : DocumentRenderer<AlertBlock>
{
    protected override void Write(DocumentMarkdownWriter renderer, AlertBlock obj)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        MdAlert alert = new(obj, renderer.Theme);

        renderer.Push(alert);
        renderer.WriteChildren(obj);

        // Fix header and bottom margin/ padding (caused by paragraph margin)
        if (alert.Section.Blocks.Count > 1)
        {
            Thickness firstMargin = alert.Section.Blocks.ElementAt(1).Margin;
            if (alert.Section.Blocks.Count == 2)
            {
                alert.Section.Blocks.ElementAt(1).Margin = new Thickness(firstMargin.Left, 0, firstMargin.Right, 0);
            }
            else
            {
                Thickness lastMargin = alert.Section.Blocks.LastBlock.Margin;
                alert.Section.Blocks.ElementAt(1).Margin = new Thickness(firstMargin.Left, 0, firstMargin.Right, firstMargin.Bottom);
                alert.Section.Blocks.LastBlock.Margin = new Thickness(lastMargin.Left, lastMargin.Top, lastMargin.Right, 0);
            }
        }

        renderer.Pop();
    }
}
