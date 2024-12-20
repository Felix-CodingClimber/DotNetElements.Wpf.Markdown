﻿using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdListItem : TextElementWithChilds
{
    public override TextElement TextElement => listItem;
    public ListItem ListItem => listItem;

    private readonly ListItem listItem;

    public MdListItem(MarkdownThemes theme)
    {
        listItem = new ListItem()
        {
            Margin = theme.ListItemParagraphMargin,
        };
    }

    public override void AddChild(TextElementBase child)
    {
        TextElement? element = child.TextElement;

        if (element is null)
            return;

        if (element is System.Windows.Documents.Block block)
        {
            listItem.Blocks.Add(block);
        }
        else if (element is System.Windows.Documents.Inline inline)
        {
            Paragraph paragraph = new();
            paragraph.Inlines.Add(inline);
            listItem.Blocks.Add(paragraph);
        }
    }
}
