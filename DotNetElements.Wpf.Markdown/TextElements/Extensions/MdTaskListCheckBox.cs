using System.Windows.Controls;
using System.Windows.Documents;
using Markdig.Extensions.TaskLists;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdTaskListCheckBox : TextElementBase
{
    public override TextElement TextElement => container;

    private readonly InlineUIContainer container;

    public MdTaskListCheckBox(TaskList taskList, MarkdownThemes themes)
    {
        container = new InlineUIContainer()
        {
            BaselineAlignment = System.Windows.BaselineAlignment.Center
        };

        CheckBox checkBox = new()
        {
            IsHitTestVisible = false,
            Focusable = false,
            IsChecked = taskList.Checked
        };

        container.Child = checkBox;
    }
}
