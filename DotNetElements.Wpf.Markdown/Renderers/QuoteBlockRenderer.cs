using System.Windows;
using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Renderers;

internal sealed class QuoteBlockRenderer : DocumentRenderer<QuoteBlock>
{
    protected override void Write(DocumentMarkdownWriter renderer, QuoteBlock obj)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        MdQuote quote = new(renderer.Theme);

        renderer.Push(quote);
        renderer.WriteChildren(obj);

        // Fix top and bottom margin/ padding (caused by paragraph margin)
        Thickness firstMargin = quote.Section.Blocks.FirstBlock.Margin;
        if (quote.Section.Blocks.Count == 1)
        {
            quote.Section.Blocks.FirstBlock.Margin = new Thickness(firstMargin.Left, 0, firstMargin.Right, 0);
        }
        else
        {
            Thickness lastMargin = quote.Section.Blocks.LastBlock.Margin;
            quote.Section.Blocks.FirstBlock.Margin = new Thickness(firstMargin.Left, 0, firstMargin.Right, firstMargin.Bottom);
            quote.Section.Blocks.LastBlock.Margin = new Thickness(lastMargin.Left, lastMargin.Top, lastMargin.Right, 0);
        }

        renderer.Pop();
    }
}
