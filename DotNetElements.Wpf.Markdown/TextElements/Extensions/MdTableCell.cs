using System.Windows;
using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdTableCell : TextElementWithChilds
{
    public override TextElement TextElement => tableCell;
    public TableCell TableCell => tableCell;

    private readonly TableCell tableCell;

    public MdTableCell(TextAlignment textAlignment, MarkdownThemes themes)
    {
        tableCell = new TableCell()
        {
            BorderBrush = themes.TableBorderBrush,
            BorderThickness = new Thickness(0, 0, themes.TableBorderThickness, themes.TableBorderThickness),
            Padding = themes.TableCellPadding,
            TextAlignment = textAlignment
        };
    }

    public override void AddChild(TextElementBase child)
    {
        TextElement? element = child.TextElement;

        if (element is null)
            return;

        if (element is Block block)
        {
            tableCell.Blocks.Add(block);
        }
        else if (element is Inline inline)
        {
            Paragraph paragraph = new();
            paragraph.Inlines.Add(inline);
            tableCell.Blocks.Add(paragraph);
        }
    }
}
