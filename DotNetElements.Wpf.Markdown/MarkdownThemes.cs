﻿using System.Windows;
using System.Windows.Media;

namespace DotNetElements.Wpf.Markdown;

public sealed class MarkdownThemes : DependencyObject
{
	internal static MarkdownThemes Default { get; } = new();

	public Thickness Padding { get; set; } = new(8);
	public Thickness InternalMargin { get; set; } = new(4);
	public CornerRadius CornerRadius { get; set; } = new(4);

    // Paragraphs

    public Thickness ParagraphMargin { get; set; } = new(left: 0, top: 0, right: 0, bottom: 20);

    // Lists
    public Thickness ListMargin { get; set; } = new(left: 5, top: 5, right: 5, bottom: 5);
    public Thickness ListItemParagraphMargin { get; set; } = new(left: 0, top: 0, right: 0, bottom: 5);

    // Headers

    public double H1FontSize { get; set; } = 22;
	public double H2FontSize { get; set; } = 20;
	public double H3FontSize { get; set; } = 18;
	public double H4FontSize { get; set; } = 16;
	public double H5FontSize { get; set; } = 14;
	public double H6FontSize { get; set; } = 12;

	public Brush HeadingForeground { get; set; } = new SolidColorBrush(Colors.Purple);

	public FontWeight H1FontWeight { get; set; } = FontWeights.Bold;
	public FontWeight H2FontWeight { get; set; } = FontWeights.Normal;
	public FontWeight H3FontWeight { get; set; } = FontWeights.Normal;
	public FontWeight H4FontWeight { get; set; } = FontWeights.Normal;
	public FontWeight H5FontWeight { get; set; } = FontWeights.Normal;
	public FontWeight H6FontWeight { get; set; } = FontWeights.Normal;

	public Thickness H1Margin { get; set; } = new(left: 0, top: 14, right: 0, bottom: 0);
	public Thickness H2Margin { get; set; } = new(left: 0, top: 14, right: 0, bottom: 0);
	public Thickness H3Margin { get; set; } = new(left: 0, top: 14, right: 0, bottom: 0);
	public Thickness H4Margin { get; set; } = new(left: 0, top: 14, right: 0, bottom: 0);
	public Thickness H5Margin { get; set; } = new(left: 0, top: 8, right: 0, bottom: 0);
	public Thickness H6Margin { get; set; } = new(left: 0, top: 8, right: 0, bottom: 0);

    // Inline code

	public Brush InlineCodeBackground { get; set; } = new SolidColorBrush(Colors.LightGray);
	public Brush InlineCodeForeground { get; set; } = new SolidColorBrush(Colors.Black);
	public Brush InlineCodeBorderBrush { get; set; } = new SolidColorBrush(Colors.Gray);
	public Thickness InlineCodeBorderThickness { get; set; } = new(1);
	public CornerRadius InlineCodeCornerRadius { get; set; } = new(2);
	public Thickness InlineCodePadding { get; set; } = new(0);
	public double InlineCodeFontSize { get; set; } = 10;
	public FontWeight InlineCodeFontWeight { get; set; } = FontWeights.Normal;

    // Thematic break
	public Brush ThematicBreakLineBrush { get; set; } = new SolidColorBrush(Colors.Gray);
	public double ThematicBreakLineThickness { get; set; } = 2;
    public Thickness ThematicBreakMargin { get; set; } = new(left: 0, top: 12, right: 0, bottom: 12);

}
