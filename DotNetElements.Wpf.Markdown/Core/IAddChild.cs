using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.Core;

public interface IAddChild
{
    // todo check nullable
    TextElement TextElement { get; }
    void AddChild(IAddChild child);
}
