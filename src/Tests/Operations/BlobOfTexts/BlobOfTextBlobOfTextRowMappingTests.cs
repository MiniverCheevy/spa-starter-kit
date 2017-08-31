using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.BlobOfTexts;
using Core.Operations.BlobOfTexts.Extras;
using Core.Models.Mappings;
using Core.Models.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
using Voodoo.TestData;
namespace Tests.Operations.BlobOfTexts
{
    [TestClass]
    public class BlobOfTextBlobOfTextRowMappingTests
    {
        private Randomizer randomizer = new Randomizer();
        
        private BlobOfText arrange()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new BlobOfText();
            TestHelper.Randomizer.Randomize(source);
            return source;
        }
        
        [TestMethod]
        public void Map_MapBack_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<BlobOfText, BlobOfTextRow>();
            var source = arrange();
            var message = source.ToBlobOfTextRow();
            var target = new BlobOfText();
            target.UpdateFrom(message);
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
        }
    }
}

