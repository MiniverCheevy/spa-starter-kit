
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Teams;
using Core.Operations.Teams.Extras;
using Voodoo.TestData;
using Core.Context;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
namespace Tests.Operations.Teams
{
    [TestClass]
    public class TeamTestHelper
    {
        public static TeamDetail GetNewTeam()
        {
            var request= new TeamDetail();
            TestHelper.Randomizer.Randomize(request);
            return request;
        }
        
        public static async Task<TeamDetail> GetExistingTeam()
        {
            var request= GetNewTeam();
            var command = new TeamSaveCommand(request);
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(TeamMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);
            
            var query = new TeamDetailQuery(new IdRequest{Id =response.NewItemId});
            var data= await query.ExecuteAsync();
            return data.Data;
        }
    }
}

