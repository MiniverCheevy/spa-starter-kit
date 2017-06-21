﻿using Fernweh.Tests.CodeGeneration.Helpers.ModelBuilders.TestClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.CodeGeneration.Helpers.ModelBuilders;
using FluentAssertions;

namespace Fernweh.Tests.CodeGeneration.Helpers.ModelBuilders
{
    
    [TestClass]
    public class TypescriptMetadataBuilderTests
    {
        

        [TestMethod]
        public void BuildDateMetadata()
        {
            var name = "test";
            var properties = typeof(DateTest).GetProperties();

            var builder = new TypescriptMetadataBuilder(name, properties);
            var output = builder.Build();

            
        }
    }
}
