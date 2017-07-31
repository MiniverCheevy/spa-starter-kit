
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Teams;
using Core.Operations.Teams.Extras;
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

namespace Tests.Operations.Teams
{
    [TestClass]
    public class TeamMappingTests
    {
        private Randomizer randomizer = new Randomizer();
        
        private Team arrange()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new Team();
            TestHelper.Randomizer.Randomize(source);
            return source;
        }
        
        [TestMethod]
        public void Map_Message_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<Team, TeamRow>();
            var source = arrange();
            var message = source.ToTeamRow();
            var target = new Team();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
            
        }
        
        [TestMethod]
        public void Map_Detail_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<Team, TeamDetail>();
            var source = arrange();
            var message = source.ToTeamDetail();
            var target = new Team();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
            
        }
        
    }
}

