using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Extensions.TaskLists;

namespace DotNetElements.Wpf.Markdown.Renderers.Extensions;

internal sealed class TaskListRenderer : DocumentRenderer<TaskList>
{
	protected override void Write(DocumentMarkdownWriter renderer, TaskList obj)
	{
		ArgumentNullException.ThrowIfNull(renderer);
		ArgumentNullException.ThrowIfNull(obj);

		MdTaskListCheckBox checkBox = new(obj, renderer.Config.Themes);

		renderer.WriteInline(checkBox);
	}
}
