using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Users;
using Core.Operations.Users.Extras;
using Core.Models.Mappings;
using Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Core.Identity;
using Voodoo;
using Voodoo.Messages;
using Voodoo.TestData;
namespace Tests.Operations.Users
{
    [TestClass]
    public class UserAppPrincipalMappingTests
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
        public void Map_MapBack_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<User, AppPrincipal>();
            var source = arrange();
            var message = source.ToAppPrincipal();
            var target = new User();
            target.UpdateFrom(message);
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
        }
    }
}

