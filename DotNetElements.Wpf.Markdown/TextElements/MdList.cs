using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdList : IAddChild
{
    public TextElement TextElement => list;

    private readonly List list;
    private readonly bool isOrdered;

    public MdList(ListBlock listBlock, MarkdownConfig config)
    {
        int startIndex = 1;

        if (listBlock.IsOrdered)
        {
            isOrdered = true;

            if (listBlock.OrderedStart is not null && (listBlock.DefaultOrderedStart != listBlock.OrderedStart))
                startIndex = int.Parse(listBlock.OrderedStart, NumberFormatInfo.InvariantInfo);
        }

        list = new List()
        {
            MarkerStyle = isOrdered ? TextMarkerStyle.Decimal : TextMarkerStyle.Disc,
            StartIndex = isOrdered ? startIndex : 1,
            Margin = config.Themes.ListMargin
        };
    }

    public void AddChild(IAddChild child)
    {
        // todo
        if (child is not MdListItem listItem)
        {
            System.Diagnostics.Debug.WriteLine($"Invalid list item child {child}"); // todo debug

            return;
        }

        list.ListItems.Add(listItem.ListItem);
    }
}
