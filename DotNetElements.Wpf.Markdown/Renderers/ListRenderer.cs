using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Renderers;

internal sealed class ListRenderer : DocumentRenderer<ListBlock>
{
	protected override void Write(DocumentMarkdownWriter renderer, ListBlock obj)
	{
        ArgumentNullException.ThrowIfNull(renderer);
        ArgumentNullException.ThrowIfNull(obj);

        MdList list = new(obj, renderer.Config);

        renderer.Push(list);

        foreach (ListItemBlock item in obj.Cast<ListItemBlock>())
        {
            MdListItem listItem = new(renderer.Config);

            renderer.Push(listItem);
            renderer.WriteChildren(item);
            renderer.Pop();
        }

        renderer.Pop();
    }
}
