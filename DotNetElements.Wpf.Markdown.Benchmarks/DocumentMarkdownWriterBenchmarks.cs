using System.Windows.Documents;
using System.Windows.Media;
using BenchmarkDotNet.Attributes;
using DotNetElements.Wpf.Markdown.Core;
using DotNetElements.Wpf.Markdown.TextElements;
using Markdig;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Benchmarks;

public class DocumentMarkdownWriterBenchmarks
{
    [Benchmark, STAThread]
    public FlowDocument RenderBenchmark()
    {
        MdFlowDocument document = new();
        document.Document.FontFamily = new FontFamily("Segoe UI"); // todo check if we want this in config
        document.Document.Background = new SolidColorBrush(Colors.White); // todo check if we want this in config
        DocumentMarkdownWriter renderer = new(document, (uri) => { }); // todo pass fixed test config

        MarkdownPipeline pipeline = new MarkdownPipelineBuilder()
            .UseEmphasisExtras()
            //.UseAutoLinks()
            .UseTaskLists() // todo check if feature is enabled in config >>> need to reset the pipeline if the config changes
            .UsePipeTables() // todo check if feature is enabled in config >>> need to reset the pipeline if the config changes
            .UseAlertBlocks() // todo check if feature is enabled in config >>> need to reset the pipeline if the config changes
            .Build();

        pipeline.Setup(renderer);

        MarkdownDocument mdDocument = Markdig.Markdown.Parse(TestMarkdown, pipeline);
        renderer.Render(mdDocument);

        return document.Document;
    }

    private const string TestMarkdown =
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
