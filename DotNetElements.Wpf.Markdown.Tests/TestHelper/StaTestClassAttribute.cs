namespace DotNetElements.Wpf.Markdown.Tests.TestHelper;

public class StaTestClassAttribute : TestClassAttribute
{
    public override TestMethodAttribute GetTestMethodAttribute(TestMethodAttribute? testMethodAttribute)
    {
        if (testMethodAttribute is StaTestMethodAttribute)
            return testMethodAttribute;

        return new StaTestMethodAttribute(base.GetTestMethodAttribute(testMethodAttribute));
    }
}
