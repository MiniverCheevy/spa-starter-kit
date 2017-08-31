using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Members;
using Core.Operations.Members.Extras;
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
namespace Tests.Operations.Members
{
    [TestClass]
    public class MemberMemberDetailMappingTests
    {
        private Randomizer randomizer = new Randomizer();
        
        private Member arrange()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new Member();
            TestHelper.Randomizer.Randomize(source);
            return source;
        }
        
        [TestMethod]
        public void Map_MapBack_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<Member, MemberDetail>();
            var source = arrange();
            var message = source.ToMemberDetail();
            var target = new Member();
            target.UpdateFrom(message);
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
        }
    }
}

