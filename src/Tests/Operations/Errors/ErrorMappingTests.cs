
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Errors;
using Core.Operations.Errors.Extras;
using Core.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
using Voodoo.TestData;
using Core.Models.Mappings;

namespace Tests.Operations.Errors
{
    [TestClass]
    public class ErrorMappingTests
    {
        private Randomizer randomizer = new Randomizer();
        
        private Error arrange()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new Error();
            TestHelper.Randomizer.Randomize(source);
            return source;
        }
        
        [TestMethod]
        public void Map_Message_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<Error, ErrorRow>();
            var source = arrange();
            var message = source.ToErrorRow();
            var target = new Error();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new string[]{});
            
        }
        
        [TestMethod]
        public void Map_Detail_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<Error, ErrorDetail>();
            var source = arrange();
            var message = source.ToErrorDetail();
            var target = new Error();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new string[]{});
            
        }
        
    }
}

