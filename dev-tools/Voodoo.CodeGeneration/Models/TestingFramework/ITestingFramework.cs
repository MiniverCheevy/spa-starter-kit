namespace Voodoo.CodeGeneration.Models.TestingFramework
{
    public interface ITestingFramework
    {
        string[] RequiredNamespaces { get; }
        string TestLevelAttribute { get; }
        string ClassLevelAttribute { get; }
    }

    public static class TestingFrameworkFactory
    {
        public static ITestingFramework GetFramework(ConfigurationFile configuration)
        {
            if (configuration.TestingFramework.To<string>().ToLower() == "xunit")
                return new XUnitTestingFramework();

            return new MsTestTestingFramework();
        }
    }
}