
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
    public class TeamDeleteCommandTests
    {
        [TestMethod]
        public async Task TeamDeleteCommand_ValidRequest_IsOk()
        {
            var model = await TeamTestHelper.GetExistingTeam();
            var request = new IdRequest { Id=model.Id };
            var command = new TeamDeleteCommand(request);
            
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(TeamMessages.DeleteOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

