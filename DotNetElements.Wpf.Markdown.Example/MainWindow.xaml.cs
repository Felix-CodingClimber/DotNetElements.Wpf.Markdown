using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DotNetElements.Wpf.Markdown.Core;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Example;

[ObservableObject]
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        MarkdownTextBlock.OnLinkClicked += MarkdownTextBlock_OnLinkClicked;
        MarkdownTextBlock.OnMarkdownParsed += MarkdownTextBlock_OnMarkdownParsed;
        MarkdownTextBlock.MarkdownParsedCommand = new RelayCommand<MarkdownDocument>(MarkdownTextBlock_OnMarkdownParsedCommand);

        MarkdownConfig config = MarkdownConfig.Default;
        config.FeaturePipeTablesSupported = false;
        MarkdownTextBlock.Config = config;

        MarkdownInput.Text = ExampleMarkdown;
    }

    private void MarkdownTextBlock_OnMarkdownParsedCommand(MarkdownDocument? markdownDocument)
    {
        System.Diagnostics.Debug.WriteLine("Markdown parsed command"); // todo debug
    }

    private void MarkdownTextBlock_OnMarkdownParsed(object? sender, MarkdownParsedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Markdown parsed event"); // todo debug
    }

    private void MarkdownTextBlock_OnLinkClicked(object? sender, LinkClickedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine($"Link clicked: {e.Uri}"); // todo debug
    }

    private void OnRefreshButton_Click(object sender, RoutedEventArgs e)
    {
        MarkdownTextBlock.Text = MarkdownInput.Text;
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

This is a [Link](http://a.com)

---

Nested style test

# Headline with some^superscript^

**bold~sub~ and^super^**

regular~sub~ and^super^

*nested italics **and bold***

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

This is a code block:
```cs
public class Test
{
    public void TestMethod()
    {
        Console.WriteLine("Hello World");
    }
}
```

---

> This is a quote

This is a quote with two lines

> First line
>
> Second line

---

This is a image `![Image](img/exampleImg1.png)`

![Image](img/exampleImg1.png)

This is a image with a defined size `![Image](img/exampleImg1.png=x100)`

![Image](img/exampleImg1.png=x100)

---

This is a task list

- [ ] item 1
- [ ] item 2
- [x] item 3 checked

---

This is a table

| Header 1 | Header 2 | Header 3 |
|----------|----------|----------|
| Row 1    | Row 1    | Row 1    |
| Row 2    | Row 2    | Row 2    |

This is a table with defined text alignment

| Header 1 (default) | Header 2 (center) | Header 3 (right) |
|--------------------|:-----------------:|-----------------:|
| Row 1              | Row 1             | Row 1            |
| Row 2              | Row 2             | Row 2            |

---

> [!NOTE]  
> Highlights information that users should take into account, even when skimming.

> [!TIP]
> Optional information to help a user be more successful.

> [!IMPORTANT]  
> Crucial information necessary for users to succeed.

> [!WARNING]  
> Critical content demanding immediate user attention due to potential risks.

> [!CAUTION]
> Negative potential consequences of an action.
""";
}