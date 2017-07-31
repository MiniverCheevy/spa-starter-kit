
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Teams;
using Core.Operations.Teams.Extras;
using Voodoo.TestData;
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
    public class TeamAddCommandTests
    {
        [TestMethod]
        public async Task TeamAddCommand_ValidRequest_IsOk()
        {
            var request = TeamTestHelper.GetNewTeam();
            var command = new TeamSaveCommand(request);
            
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(TeamMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);
        }
    }
}

