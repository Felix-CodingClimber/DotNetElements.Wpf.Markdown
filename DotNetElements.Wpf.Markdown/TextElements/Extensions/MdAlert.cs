using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using DotNetElements.Wpf.Markdown.Core;
using Markdig.Extensions.Alerts;

namespace DotNetElements.Wpf.Markdown.TextElements;

internal sealed class MdAlert : IAddChild
{
    public TextElement TextElement => section;
    public Section Section => section;

    private readonly Section section;

    public MdAlert(AlertBlock alertBlock, MarkdownConfig config)
    {
        string kind = alertBlock.Kind.ToString();

        section = new Section
        {
            BorderBrush = GetBrushForKind(kind, config.Themes),
            BorderThickness = config.Themes.AlertBorderThickness,
            Background = config.Themes.AlertBackground,
            Foreground = config.Themes.AlertForeground,
            Padding = config.Themes.AlertPadding,
            Margin = config.Themes.AlertMargin,
            FontSize = config.Themes.AlertHeaderFontSize
        };

        Run header = new Run(kind)
        {
            Foreground = GetBrushForKind(kind, config.Themes),
            FontWeight = FontWeights.Bold,
            FontSize = config.Themes.AlertHeaderFontSize,
        };

        Paragraph headerParagraph = new Paragraph(header)
        {
            Margin = new Thickness(left: 0, top: 0, right: 0, bottom: config.Themes.AlertHeaderSpacing)
        };

        section.Blocks.Add(headerParagraph);
    }

    public void AddChild(IAddChild child)
    {
        TextElement? element = child.TextElement;

        if (element is null)
            return;

        if (element is Block block)
        {
            section.Blocks.Add(block);
        }
        else if (element is Inline inline)
        {
            Paragraph paragraph = new();
            paragraph.Inlines.Add(inline);
            section.Blocks.Add(paragraph);
        }
    }

    private static Brush GetBrushForKind(string kind, MarkdownThemes themes)
    {
        return kind switch
        {
            "NOTE" => themes.AlertNoteAccent,
            "TIP" => themes.AlertTipAccent,
            "IMPORTANT" => themes.AlertImportantAccent,
            "WARNING" => themes.AlertWarningAccent,
            "CAUTION" => themes.AlertCautionAccent,
            _ => themes.AlertForeground,
        };
    }
}
