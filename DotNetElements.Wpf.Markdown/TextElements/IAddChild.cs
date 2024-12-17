using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

public interface IAddChild
{
	// todo check nullable
	TextElement TextElement { get; }
	void AddChild(IAddChild child);
}
