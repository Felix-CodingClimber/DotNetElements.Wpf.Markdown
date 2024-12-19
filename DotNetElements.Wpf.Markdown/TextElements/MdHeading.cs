using Markdig.Syntax;
using System.Windows.Documents;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdHeading : IAddChild
{
	public TextElement TextElement => paragraph;

	private readonly Paragraph paragraph;

	public MdHeading(HeadingBlock headingBlock, MarkdownConfig config)
	{
		paragraph = new Paragraph();

		SetProperties(headingBlock.Level, config);
	}

	public void AddChild(IAddChild child)
	{
        // todo
		if (child.TextElement is not Inline inlineChild)
        {
            System.Diagnostics.Debug.WriteLine($"Invalid heading child {child}"); // todo debug

            return;
        }

        if (child is ICascadeChild cascadeChild)
            cascadeChild.InheritProperties(this);

        paragraph.Inlines.Add(inlineChild);
	}

	private void SetProperties(int level, MarkdownConfig config)
	{
		paragraph.FontSize = level switch
		{
			1 => config.Themes.H1FontSize,
			2 => config.Themes.H2FontSize,
			3 => config.Themes.H3FontSize,
			4 => config.Themes.H4FontSize,
			5 => config.Themes.H5FontSize,
			_ => config.Themes.H6FontSize,
		};

		paragraph.Foreground = config.Themes.HeadingForeground;

		paragraph.FontWeight = level switch
		{
			1 => config.Themes.H1FontWeight,
			2 => config.Themes.H2FontWeight,
			3 => config.Themes.H3FontWeight,
			4 => config.Themes.H4FontWeight,
			5 => config.Themes.H5FontWeight,
			_ => config.Themes.H6FontWeight,
		};

		paragraph.Margin = level switch
		{
			1 => config.Themes.H1Margin,
			2 => config.Themes.H2Margin,
			3 => config.Themes.H3Margin,
			4 => config.Themes.H4Margin,
			5 => config.Themes.H5Margin,
			_ => config.Themes.H6Margin,
		};
	}
}
