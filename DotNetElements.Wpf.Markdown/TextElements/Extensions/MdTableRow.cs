using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdTableRow : IAddChild
{
    public TextElement TextElement => tableRow;
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

    public void AddChild(IAddChild child)
    {
        // todo
        if (child is not MdTableCell tableCell)
        {
            System.Diagnostics.Debug.WriteLine($"Invalid table row child {child}"); // todo debug

            return;
        }

        tableRow.Cells.Add(tableCell.TableCell);
    }
}
