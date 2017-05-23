using System.Configuration;
using System.Diagnostics;
using Core;
using Core.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tests.Fakes;

namespace Tests
{
    [TestClass]
    public class AssemblyInitialize
    {
        [TestMethod]
        public void Test()
        {
        }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext)
        {
            Debug.WriteLine("AssemblyInit");
            IOC.RequestContextProvier = new FakeRequestContextProvider();
#if DEBUG
            IOC.ContextFactory = new ContextFactory();
            IOC.Settings = new Settings
            {
                DefaultConnectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString()
            };
#else
            IOC.ContextFactory = new FakeContextFactory();
#endif
        }
    }
}