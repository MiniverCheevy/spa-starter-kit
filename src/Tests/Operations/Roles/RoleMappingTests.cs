
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Roles;
using Core.Operations.Roles.Extras;
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

namespace Tests.Operations.Roles
{
    [TestClass]
    public class RoleMappingTests
    {
        private Randomizer randomizer = new Randomizer();
        
        private Role arrange()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new Role();
            TestHelper.Randomizer.Randomize(source);
            return source;
        }
        
        [TestMethod]
        public void Map_Message_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<Role, RoleRow>();
            var source = arrange();
            var message = source.ToRoleRow();
            var target = new Role();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
            
        }
        
        [TestMethod]
        public void Map_Detail_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<Role, RoleDetail>();
            var source = arrange();
            var message = source.ToRoleDetail();
            var target = new Role();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
            
        }
        
    }
}

