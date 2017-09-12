using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Fernweh.Tests.CodeGeneration.Helpers.ModelBuilders.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo;
using Voodoo.CodeGeneration.Helpers.ModelBuilders;

namespace Voodoo.CodeGeneration.Tests.CodeGeneration.Helpers.ModelBuilders
{
    
    [TestClass]
    public class TypescriptMetadataBuilderTests
    {
        

        [TestMethod]
        public void BuildDateMetadata()
        {
            var name = "test";
            var properties = typeof(DateTest).GetProperties();

            var builder = new TypescriptMetadataBuilder(typeof(DateTest), properties);
            var output = builder.Build();
            Debug.WriteLine(output);
            
        }
    }
}
