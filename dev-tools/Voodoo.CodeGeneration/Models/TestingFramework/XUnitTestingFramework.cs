namespace Voodoo.CodeGeneration.Models.TestingFramework
{
    public class XUnitTestingFramework : ITestingFramework
    {
        public XUnitTestingFramework()
        {
            RequiredNamespaces = new[] {"Xunit"};
            TestLevelAttribute = "[Fact]";
            ClassLevelAttribute = string.Empty;
        }

        public string[] RequiredNamespaces { get; }
        public string TestLevelAttribute { get; }
        public string ClassLevelAttribute { get; }
    }
}