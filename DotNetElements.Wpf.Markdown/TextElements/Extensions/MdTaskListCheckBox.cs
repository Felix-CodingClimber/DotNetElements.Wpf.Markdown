using System.Windows.Controls;
using System.Windows.Documents;
using Markdig.Extensions.TaskLists;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdTaskListCheckBox : IAddChild
{
    public TextElement TextElement => container;

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

    // Not used here
    public void AddChild(IAddChild child) => throw new InvalidOperationException();
}
