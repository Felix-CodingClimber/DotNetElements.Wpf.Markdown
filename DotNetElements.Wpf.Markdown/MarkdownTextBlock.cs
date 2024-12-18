using DotNetElements.Wpf.Markdown.TextElements;
using Markdig;
using Markdig.Syntax;
using System.Windows;
using System.Windows.Controls;

namespace DotNetElements.Wpf.Markdown;

[TemplatePart(Name = MarkdownContainerName, Type = typeof(Grid))]
public partial class MarkdownTextBlock : Control
{
    private static readonly DependencyProperty ConfigProperty = DependencyProperty.Register(
        nameof(Config),
        typeof(MarkdownConfig),
        typeof(MarkdownTextBlock),
        new PropertyMetadata(null, OnConfigChanged)
    );

    private static void OnConfigChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is MarkdownTextBlock self && e.NewValue is not null)
            self.ApplyConfig(self.Config);
    }

    public MarkdownConfig Config
    {
        get => (MarkdownConfig)GetValue(ConfigProperty);
        set => SetValue(ConfigProperty, value);
    }

    private static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(Text),
        typeof(string),
        typeof(MarkdownTextBlock),
        new PropertyMetadata(null, OnTextChanged));

    private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not MarkdownTextBlock self)
            throw new InvalidOperationException();

        if (e.NewValue is not null)
            self.ApplyText(true);
        else
            self.ClearText();
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    private static readonly DependencyProperty MarkdownDocumentProperty = DependencyProperty.Register(
        nameof(MarkdownDocument),
        typeof(MarkdownDocument),
        typeof(MarkdownTextBlock),
        new PropertyMetadata(null));

    public MarkdownDocument? MarkdownDocument
    {
        get => (MarkdownDocument)GetValue(MarkdownDocumentProperty);
        private set => SetValue(MarkdownDocumentProperty, value);
    }

    public event EventHandler<LinkClickedEventArgs>? OnLinkClicked;

    internal void RaiseLinkClickedEvent(Uri uri) => OnLinkClicked?.Invoke(this, new LinkClickedEventArgs(uri));

    private const string MarkdownContainerName = "MarkdownContainer";

    private FlowDocumentScrollViewer? container;
    private MarkdownPipeline pipeline;
    private MdFlowDocument document;
    private DocumentMarkdownWriter? renderer;

    static MarkdownTextBlock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownTextBlock), new FrameworkPropertyMetadata(typeof(MarkdownTextBlock)));
    }

    public MarkdownTextBlock()
    {
        document = new MdFlowDocument();
        pipeline = new MarkdownPipelineBuilder()
            .UseEmphasisExtras()
            //.UseAutoLinks()
            //.UseTaskLists()
            //.UsePipeTables()
            .Build();
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        container = (FlowDocumentScrollViewer)GetTemplateChild(MarkdownContainerName);
        ArgumentNullException.ThrowIfNull(container);
        container.Document = document.Document;

        Build();
    }

    private void ApplyConfig(MarkdownConfig config)
    {
        if (renderer is null)
            return;

        renderer.Config = config;
    }

    private void ApplyText(bool rerender)
    {
        if (renderer is null)
            return;

        if (rerender)
            renderer.ReloadDocument();

        if (!string.IsNullOrEmpty(Text))
        {
            MarkdownDocument = Markdig.Markdown.Parse(Text, pipeline);
            renderer.Render(MarkdownDocument);
        }
    }

    private void ClearText()
    {
        if (renderer is null)
            return;

        renderer.ClearDocument();
    }

    private void Build()
    {
        renderer ??= new DocumentMarkdownWriter(document, this, Config);
        document.Document.FontFamily = this.FontFamily; // todo check if we want this in config
        document.Document.Background = this.Background; // todo check if we want this in config

        pipeline.Setup(renderer);

        ApplyText(false);
    }
}
