
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
    public class TeamUpdateCommandTests
    {
        [TestMethod]
        public async Task TeamUpdateCommand_ValidRequest_IsOk()
        {
            var request = await TeamTestHelper.GetExistingTeam();
            var command = new TeamSaveCommand(request);
            var response = await command.ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(TeamMessages.UpdateOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

