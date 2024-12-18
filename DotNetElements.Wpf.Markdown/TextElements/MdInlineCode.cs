﻿using System.Windows.Documents;
using Markdig.Syntax.Inlines;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdInlineCode : IAddChild
{
	public TextElement TextElement => run;

	private readonly Run run;

	public MdInlineCode(CodeInline codeInline, MarkdownConfig config)
	{
        run = new Run(codeInline.Content)
        {
            Background = config.Themes.InlineCodeBackground,
            Foreground = config.Themes.InlineCodeForeground,
            FontSize = config.Themes.InlineCodeFontSize,
            FontWeight = config.Themes.InlineCodeFontWeight
        };
        //run.Padding = config.Themes.InlineCodePadding; // todo not working
    }

    // Not used here
    public void AddChild(IAddChild child) => throw new InvalidOperationException();
}
