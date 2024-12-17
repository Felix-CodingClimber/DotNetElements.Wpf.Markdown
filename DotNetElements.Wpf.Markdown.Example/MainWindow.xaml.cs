﻿using System.Windows;

namespace DotNetElements.Wpf.Markdown.Example;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		MarkdownTextBlock.Text = ExampleMarkdown;
        MarkdownTextBlock.OnLinkClicked += MarkdownTextBlock_OnLinkClicked;
	}

    private void MarkdownTextBlock_OnLinkClicked(object? sender, LinkClickedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"Clicked link {e.Uri}"); // todo debug
    }

    private const string ExampleMarkdown =
"""
# Heading 1
## Heading 2
### Heading 3
#### Heading 4
##### Heading 5

This is a simple paragraph

This is a very long paragraph which should break in the flow document automatically. Somewhere around here should be a line break.

This is a **Bold** inline text

This is a *Italic* inline text

This is a ~~Strikethrough~~ inline text

This is a ++Underline++ inline text

This is a ==Marked== inline text

This is a inline `code` element

---

This is a unordered list:

- Item 1
- Item 2 **Bold inline element**
- Item 3 **First inline element** *Second inline element* third default inline element

This is a ordered list:
1. Item 1
2. Item 2 **Bold inline element**
3. Item 3 **First inline element** *Second inline element* third default inline element

This is another ordered list:
1. Second list item 1
2. Second list item 2
3. Second list item 3

This is a nested unordered list:
- Item 1
  - Nested item 1
  - Nested item 2
- Item 2

This is a nested ordered list:
1. Item 1
   1. Nested item 1
   2. Nested item 2
2. Item 2

---

This is a [Link](http://a.com)
""";
}