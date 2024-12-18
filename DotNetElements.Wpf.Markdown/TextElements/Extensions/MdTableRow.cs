using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdTableRow : IAddChild
{
    public TextElement TextElement => tableRow;
    public TableRow TableRow => tableRow;

    public bool IsHeader { get; private set; }

    private readonly TableRow tableRow;

    public MdTableRow()
    {
        tableRow = new TableRow();
    }

    public void AddChild(IAddChild child)
    {
        // todo
        if (child is not MdTableCell tableCell)
        {
            System.Diagnostics.Debug.WriteLine($"Invalid table row child {child}"); // todo debug

            return;
        }

        if(tableCell.IsHeader)
            IsHeader = true;

        tableRow.Cells.Add(tableCell.TableCell);
    }
}
