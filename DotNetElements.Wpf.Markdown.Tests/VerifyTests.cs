namespace DotNetElements.Wpf.Markdown.Tests;

[TestClass]
[UsesVerify]
public partial class VerifyTests
{
    [TestMethod]
    public Task Run() => VerifyChecks.Run();
}