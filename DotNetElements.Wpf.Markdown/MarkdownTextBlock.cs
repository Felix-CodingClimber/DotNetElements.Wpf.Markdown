using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DotNetElements.Wpf.Markdown.TextElements;
using Markdig;
using Markdig.Syntax;

namespace DotNetElements.Wpf.Markdown;

[TemplatePart(Name = MarkdownContainerName, Type = typeof(Grid))]
public partial class MarkdownTextBlock : Control
{
    /// <summary>
    /// Event raised when a markdown link is clicked.
    /// </summary>
    public event EventHandler<LinkClickedEventArgs>? OnLinkClicked;

    /// <summary>
    /// Event raised when markdown is done parsing, with a complete MarkdownDocument.
    /// It is always raised before the control renders the document.
    /// </summary>
    public event EventHandler<MarkdownParsedEventArgs>? OnMarkdownParsed;

    private static readonly DependencyProperty ThemeProperty = DependencyProperty.Register(
        nameof(Theme),
        typeof(MarkdownThemes),
        typeof(MarkdownTextBlock),
        new PropertyMetadata(null, OnThemeChanged)
    );

    private static void OnThemeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not MarkdownTextBlock self)
            throw new InvalidOperationException();

        if (e.NewValue is not null)
            self.ApplyTheme(self.Theme);
    }

    public MarkdownThemes Theme
    {
        get => (MarkdownThemes)GetValue(ThemeProperty);
        set => SetValue(ThemeProperty, value);
    }

    private static readonly DependencyProperty ConfigProperty = DependencyProperty.Register(
        nameof(Config),
        typeof(MarkdownConfig),
        typeof(MarkdownTextBlock),
        new PropertyMetadata(null, OnConfigChanged)
    );

    private static void OnConfigChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not MarkdownTextBlock self)
            throw new InvalidOperationException();

        if (e.NewValue is not null)
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

    private static readonly DependencyPropertyKey MarkdownDocumentPropertyKey = DependencyProperty.RegisterReadOnly(
        nameof(MarkdownDocument),
        typeof(MarkdownDocument),
        typeof(MarkdownTextBlock),
        new PropertyMetadata(null));

    public MarkdownDocument? MarkdownDocument
    {
        get => (MarkdownDocument)GetValue(MarkdownDocumentPropertyKey.DependencyProperty);
        private set => SetValue(MarkdownDocumentPropertyKey, value);
    }

    private static readonly DependencyProperty MarkdownParsedCommandProperty = DependencyProperty.Register(
        nameof(MarkdownParsedCommand),
        typeof(ICommand),
        typeof(MarkdownTextBlock),
        new PropertyMetadata(null)
    );

    public ICommand MarkdownParsedCommand
    {
        get => (ICommand)GetValue(MarkdownParsedCommandProperty);
        set => SetValue(MarkdownParsedCommandProperty, value);
    }

    internal void RaiseLinkClickedEvent(Uri uri) => OnLinkClicked?.Invoke(this, new LinkClickedEventArgs(uri));

    private const string MarkdownContainerName = "MarkdownContainer";

    private FlowDocumentScrollViewer? container;

    private readonly MdFlowDocument document;
    private MarkdownPipeline pipeline;
    private DocumentMarkdownWriter? renderer;

    static MarkdownTextBlock()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(MarkdownTextBlock), new FrameworkPropertyMetadata(typeof(MarkdownTextBlock)));
    }

    public MarkdownTextBlock()
    {
        document = new MdFlowDocument();

        MarkdownConfig defaultConfig = MarkdownConfig.Default;

        MarkdownPipelineBuilder pipelineBuilder = new MarkdownPipelineBuilder();

        if (defaultConfig.FeatureEmphasisExtrasSupported)
            pipelineBuilder.UseEmphasisExtras();

        if (defaultConfig.FeatureTaskListSupported)
            pipelineBuilder.UseTaskLists();

        if (defaultConfig.FeaturePipeTablesSupported)
            pipelineBuilder.UsePipeTables();

        if (defaultConfig.FeatureAlertBlocksSupported)
            pipelineBuilder.UseAlertBlocks();

        pipeline = pipelineBuilder.Build();
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

        MarkdownPipelineBuilder pipelineBuilder = new MarkdownPipelineBuilder();

        if (config.FeatureEmphasisExtrasSupported)
            pipelineBuilder.UseEmphasisExtras();

        if (config.FeatureTaskListSupported)
            pipelineBuilder.UseTaskLists();

        if (config.FeaturePipeTablesSupported)
            pipelineBuilder.UsePipeTables();

        if (config.FeatureAlertBlocksSupported)
            pipelineBuilder.UseAlertBlocks();

        pipeline = pipelineBuilder.Build();

        ApplyText(true);
    }

    private void ApplyTheme(MarkdownThemes theme)
    {
        if (renderer is null)
            return;

        renderer.Theme = theme;
        ApplyText(true);
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

            OnMarkdownParsed?.Invoke(this, new MarkdownParsedEventArgs(MarkdownDocument));
            MarkdownParsedCommand?.Execute(MarkdownDocument);

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
        renderer ??= new DocumentMarkdownWriter(document, RaiseLinkClickedEvent, Config);
        document.Document.FontFamily = this.FontFamily; // todo check if we want this in config
        document.Document.Background = this.Background; // todo check if we want this in config

        pipeline.Setup(renderer);

        ApplyText(false);
    }
}
