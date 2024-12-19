﻿using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdList : IAddChild
{
    public TextElement TextElement => list;

    private readonly List list;
    private readonly bool isOrdered;

    private static readonly Thickness unorderedPadding = new(left: 15, top: 0, right: 0, bottom: 0);
    private static readonly Thickness orderedPadding = new(left: 20, top: 0, right: 0, bottom: 0);

    public MdList(ListBlock listBlock, MarkdownThemes theme)
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
            Margin = theme.ListMargin,
            Padding = isOrdered ? orderedPadding : unorderedPadding
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
