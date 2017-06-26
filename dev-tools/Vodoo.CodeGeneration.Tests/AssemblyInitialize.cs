using System.Configuration;
using System.Diagnostics;
//using Fernweh.Core;
//using Fernweh.Core.Infrastructure;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Logging;

namespace Fernweh.Tests 
{
    [TestClass]
    public class AssemblyInitialize
    {
        [TestMethod]
        public void Test()
        { }

        [AssemblyInitialize]
        public static void AssemblyInit(TestContext testContext)
        {
              
        }
    }
}
