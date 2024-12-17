## About

This project provides a simple markdown renderer for WPF.
This implementation uses the popular [Markdig](https://github.com/xoofx/markdig) library for parsing.
At the current state, not all markdown features are supported. Styling options for the markdown blocks need to be improved.

### Supported features:
-[x] Paragraphs
-[x] InlineEmphasis
-[x] Headings
-[x] InlineCode
-[x] InlineHyperlink 
-[x] Lists (unordered & ordered)
-[x] ThematicBreak

### Features to implement:
-[ ] CodeBlocks
-[ ] Images
-[ ] Tables

## Recommended setup

1. Install nuget package `> dotnet add package DotNetElements.Wpf.Markdown --version <insert-latest-version-here>`

2. Add styles to `App.xaml`
```xaml
<Application.Resources>
    <ResourceDictionary>
        <ResourceDictionary.MergedDictionaries>
            <ResourceDictionary Source="pack://application:,,,/DotNetElements.Wpf.Markdown;Component/Themes/Generic.xaml" />
        </ResourceDictionary.MergedDictionaries>
    </ResourceDictionary>
</Application.Resources>
```

2. Add xaml namespace
```xaml
xmlns:markdown="clr-namespace:DotNetElements.Wpf.Markdown;assembly=DotNetElements.Wpf.Markdown"
```

3. Add `MarkdownTextBlock`
```xaml
<markdown:MarkdownTextBlock x:Name="MarkdownTextBlock" FontFamily="Segoe UI" />
```

4. Set the `Text` property (binding is supported)
```cs
MarkdownTextBlock.Text = "Hello world from **DotNetElements.Wpf.Markdown**";
```

5. To change the styling options for the different markdown blocks, add a customized `MarkdownThemes.cs`
```cs
MarkdownThemes myTheme = MarkdownThemes.Default;
myTheme.InlineCodeBackground = new SolidColorBrush(Colors.HotPink);

MarkdownConfig myConfig = MarkdownConfig.Default;
myConfig.Themes = myTheme;

MarkdownTextBlock.Config = myConfig;
```

## Third party notices

The project is a port of the CommunityToolkit MarkdownTextBlock component to the WPF framework
see [CommunityToolkit/Labs-Windows/MarkdownTextBlock](https://github.com/CommunityToolkit/Labs-Windows/tree/a37acd33031037daa4d39318e3a10741b1c046ea/components/MarkdownTextBlock)