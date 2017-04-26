namespace Voodoo.CodeGeneration.Models.TestingFramework
{
    public class MsTestTestingFramework : ITestingFramework
    {
        public MsTestTestingFramework()
        {
            RequiredNamespaces = new[] {"Microsoft.VisualStudio.TestTools.UnitTesting"};
            TestLevelAttribute = "[TestMethod]";
            ClassLevelAttribute = "[TestClass]";
        }

        public string[] RequiredNamespaces { get; }
        public string TestLevelAttribute { get; }
        public string ClassLevelAttribute { get; }
    }
}