
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Users;
using Core.Operations.Users.Extras;
using Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
using Voodoo.TestData;

namespace Tests.Operations.Users
{
    [TestClass]
    public class UserMappingTests
    {
        private Randomizer randomizer = new Randomizer();
        
        private User arrange()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new User();
            TestHelper.Randomizer.Randomize(source);
            return source;
        }
        
        [TestMethod]
        public void Map_Message_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<User, UserRow>();
            var source = arrange();
            var message = source.ToUserRow();
            var target = new User();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
            
        }
        
        [TestMethod]
        public void Map_Detail_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<User, UserDetail>();
            var source = arrange();
            var message = source.ToUserDetail();
            var target = new User();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
            
        }
        
    }
}

