using System.Windows;
using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Extensions.Tables;

namespace DotNetElements.Wpf.Markdown.Renderers;

internal sealed class TableRenderer : DocumentRenderer<Table>
{
    protected override void Write(DocumentMarkdownWriter renderer, Table obj)
    {
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        MdTable mdTable = new(renderer.Config.Themes);

        renderer.Push(mdTable);

        for (int rowIndex = 0; rowIndex < obj.Count; rowIndex++)
        {
            TableRow row = (TableRow)obj[rowIndex];

            MdTableRow mdTableRow = new();

            renderer.Push(mdTableRow);

            for (int columnIndex = 0; columnIndex < row.Count; columnIndex++)
            {
                TableCell cell = (TableCell)row[columnIndex];

                TextAlignment textAlignment = TextAlignment.Left;

                if (obj.ColumnDefinitions.Count > 0)
                {
                    TableColumnAlign? alignment = obj.ColumnDefinitions[columnIndex].Alignment;

                    textAlignment = alignment switch
                    {
                        TableColumnAlign.Center => TextAlignment.Center,
                        TableColumnAlign.Left => TextAlignment.Left,
                        TableColumnAlign.Right => TextAlignment.Right,
                        _ => TextAlignment.Left,
                    };
                }

                MdTableCell mdTableCell = new(textAlignment, row.IsHeader, renderer.Config.Themes);

                renderer.Push(mdTableCell);
                renderer.Write(cell);
                renderer.Pop();
            }

            renderer.Pop();
        }

        renderer.Pop();
    }
}
