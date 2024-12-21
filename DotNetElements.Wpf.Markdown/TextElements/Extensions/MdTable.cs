using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdTable : TextElementWithChilds
{
    public override TextElement TextElement => table;

    private readonly Table table;

    public MdTable(MarkdownThemes themes)
    {
        this.table = new Table()
        {
            CellSpacing = 0,
            BorderBrush = themes.TableBorderBrush,
            BorderThickness = new System.Windows.Thickness(themes.TableBorderThickness, themes.TableBorderThickness, 0, 0),
        };
    }

    public override void AddChild(TextElementBase child)
    {
        // todo
        if (child is not MdTableRow tableRow)
        {
            System.Diagnostics.Debug.WriteLine($"Invalid table child {child}"); // todo debug

            return;
        }

        if (tableRow.IsHeader)
        {
            TableRowGroup headerRowGroup = new();

            for (int columnIndex = 0; columnIndex < tableRow.TableRow.Cells.Count; columnIndex++)
                table.Columns.Add(new TableColumn());

            headerRowGroup.Rows.Add(tableRow.TableRow);
            table.RowGroups.Add(headerRowGroup);
        }
        else
        {
            TableRowGroup bodyRowGroup = new();

            bodyRowGroup.Rows.Add(tableRow.TableRow);
            table.RowGroups.Add(bodyRowGroup);
        }
    }
}
