using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdInlineText : IAddChild
{
	public TextElement TextElement => run;
	public Run Run => run;

	private readonly Run run;

	public MdInlineText(string text)
	{
		run = new Run()
		{
			Text = text
		};
	}

	// Not used here
	public void AddChild(IAddChild child) => throw new InvalidOperationException();
}
