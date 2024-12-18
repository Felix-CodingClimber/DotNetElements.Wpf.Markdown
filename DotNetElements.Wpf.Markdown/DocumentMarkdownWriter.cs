using DotNetElements.Wpf.Markdown.Renderers;
using DotNetElements.Wpf.Markdown.Renderers.Inlines;
using DotNetElements.Wpf.Markdown.TextElements;
using Markdig.Helpers;
using Markdig.Renderers;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown;

internal class DocumentMarkdownWriter : RendererBase
{
    public MarkdownTextBlock MarkdownTextBlock { get; }
    public MdFlowDocument FlowDocument { get; private set; }
    public MarkdownConfig Config { get; set; } = MarkdownConfig.Default;

    private readonly Stack<IAddChild> stack = [];
    private char[] buffer;

    public DocumentMarkdownWriter(MdFlowDocument document, MarkdownTextBlock markdownTextBlock, MarkdownConfig? config = null)
    {
        FlowDocument = document;
        MarkdownTextBlock = markdownTextBlock;

        if (config is not null)
            Config = config;

        buffer = new char[1024];

        // Set style
        stack.Push(FlowDocument);
        LoadOverriddenRenderers();
    }

    private void LoadOverriddenRenderers()
    {
        LoadRenderers();
    }

    public override object Render(MarkdownObject markdownObject)
    {
        Write(markdownObject);

        return FlowDocument ?? new();
    }

    public void ClearDocument()
    {
        stack.Clear();
        FlowDocument.Document.Blocks.Clear();
        stack.Push(FlowDocument);
    }

    public void ReloadDocument()
    {
        stack.Clear();
        FlowDocument.Document.Blocks.Clear();
        stack.Push(FlowDocument);
        LoadOverriddenRenderers();
    }

    public void WriteLeafInline(LeafBlock leafBlock)
    {
        if (leafBlock is null || leafBlock.Inline is null)
            throw new ArgumentNullException(nameof(leafBlock));

        Markdig.Syntax.Inlines.Inline inline = leafBlock.Inline;

        while (inline is not null)
        {
            Write(inline);
            inline = inline.NextSibling; // todo null!?
        }
    }

    public void WriteLeafRawLines(LeafBlock leafBlock)
    {
        ArgumentNullException.ThrowIfNull(leafBlock);

        if (leafBlock.Lines.Lines is null)
            return;

        StringLineGroup lines = leafBlock.Lines;
        StringLine[] slices = lines.Lines;

        for (int i = 0; i < lines.Count; i++)
        {
            if (i != 0)
                WriteInline(new MdLineBreak());

            WriteText(ref slices[i].Slice);
        }
    }

    public void Push(IAddChild child)
    {
        stack.Push(child);
    }

    public void Pop()
    {
        IAddChild popped = stack.Pop();
        stack.Peek().AddChild(popped);
    }

    public void WriteBlock(IAddChild obj)
    {
        stack.Peek().AddChild(obj);
    }

    public void WriteInline(IAddChild inline)
    {
        AddInline(stack.Peek(), inline);
    }

    public void WriteText(ref StringSlice slice)
    {
        if (slice.Start > slice.End)
            return;

        WriteText(slice.Text, slice.Start, slice.Length);
    }

    public void WriteText(string? text)
    {
        WriteInline(new MdInlineText(text ?? ""));
    }

    public void WriteText(string? text, int offset, int length)
    {
        if (text is null)
            return;

        if (offset == 0 && text.Length == length)
        {
            WriteText(text);
        }
        else
        {
            if (length > buffer.Length)
            {
                buffer = text.ToCharArray();
                WriteText(new string(buffer, offset, length));
            }
            else
            {
                text.CopyTo(offset, buffer, 0, length);
                WriteText(new string(buffer, 0, length));
            }
        }
    }

    private static void AddInline(IAddChild parent, IAddChild inline)
    {
        parent.AddChild(inline);
    }

    protected virtual void LoadRenderers()
    {
        // Default block renderers
        ObjectRenderers.Add(new CodeBlockRenderer());
        ObjectRenderers.Add(new ListRenderer());
        ObjectRenderers.Add(new HeadingRenderer());
        ObjectRenderers.Add(new ParagraphRenderer());
        ObjectRenderers.Add(new QuoteBlockRenderer());
        ObjectRenderers.Add(new ThematicBreakRenderer());
        //ObjectRenderers.Add(new HtmlBlockRenderer());

        // Default inline renderers
        //ObjectRenderers.Add(new AutoLinkInlineRenderer());
        ObjectRenderers.Add(new CodeInlineRenderer());
        //ObjectRenderers.Add(new DelimiterInlineRenderer());
        ObjectRenderers.Add(new EmphasisInlineRenderer());
        //ObjectRenderers.Add(new HtmlEntityInlineRenderer());
        ObjectRenderers.Add(new LineBreakInlineRenderer());
        ObjectRenderers.Add(new LinkInlineRenderer());
        ObjectRenderers.Add(new LiteralInlineRenderer());
        //ObjectRenderers.Add(new ContainerInlineRenderer());

        // Extension renderers
        //ObjectRenderers.Add(new TableRenderer());

        if (Config.FeatureTaksListSupported)
            ObjectRenderers.Add(new TaskListRenderer());

        //ObjectRenderers.Add(new HtmlInlineRenderer());
    }
}
