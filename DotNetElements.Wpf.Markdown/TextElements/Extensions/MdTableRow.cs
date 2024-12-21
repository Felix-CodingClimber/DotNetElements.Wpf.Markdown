using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdTableRow : TextElementWithChilds
{
    public override TextElement TextElement => tableRow;
    public TableRow TableRow => tableRow;

    public bool IsHeader { get; private set; }

    private readonly TableRow tableRow;

    public MdTableRow(Markdig.Extensions.Tables.TableRow tableRow, MarkdownThemes themes)
    {
        this.tableRow = new TableRow();

        IsHeader = tableRow.IsHeader;

        if (IsHeader)
            this.tableRow.Background = themes.TableHeaderBackground;
        else
            this.tableRow.Background = themes.TableBackground;

    }

    public override void AddChild(TextElementBase child)
    {
        if (child is not MdTableCell tableCell)
            throw new InvalidOperationException($"Invalid table row child {child}");

        tableRow.Cells.Add(tableCell.TableCell);
    }
}
