using Markdig.Renderers;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown.Core;

internal abstract class DocumentRenderer<TObject> : MarkdownObjectRenderer<DocumentMarkdownWriter, TObject>
    where TObject : MarkdownObject;
