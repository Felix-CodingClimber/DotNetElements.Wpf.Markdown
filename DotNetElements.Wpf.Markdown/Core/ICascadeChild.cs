namespace DotNetElements.Wpf.Markdown.Core;

/// <summary>
/// Interface for elements that inherit properties from their parent.
/// </summary>
public interface ICascadeChild
{
    void InheritProperties(IAddChild parent);
}