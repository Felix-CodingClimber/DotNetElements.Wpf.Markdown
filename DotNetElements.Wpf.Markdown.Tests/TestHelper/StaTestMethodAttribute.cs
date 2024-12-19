namespace DotNetElements.Wpf.Markdown.Tests.TestHelper;

public class StaTestMethodAttribute : TestMethodAttribute
{
    private readonly TestMethodAttribute testMethodAttribute;

    public StaTestMethodAttribute()
    {
    }

    public StaTestMethodAttribute(TestMethodAttribute testMethodAttribute)
    {
        this.testMethodAttribute = testMethodAttribute;
    }

    public override TestResult[] Execute(ITestMethod testMethod)
    {
        if (Thread.CurrentThread.GetApartmentState() == ApartmentState.STA)
            return Invoke(testMethod);

        TestResult[] result = [];
        var thread = new Thread(() => result = Invoke(testMethod));
        thread.SetApartmentState(ApartmentState.STA);
        thread.Start();
        thread.Join();

        return result;
    }

    private TestResult[] Invoke(ITestMethod testMethod)
    {
        if (testMethodAttribute is not null)
            return testMethodAttribute.Execute(testMethod);

        return [testMethod.Invoke(null)];
    }
}
