using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Teams;
using Core.Operations.Teams.Extras;
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
    public class TeamQueryTests
    {
        [TestMethod]
        public async Task TeamDetailQuery_ValidRequest_IsOk()
        {
            int? id = 0;
            using (var context = IOC.GetContext())
            {
                if (context.Teams.Any())
                id = context.Teams.Max(c => c.Id);
            }
            id.Should().HaveValue().Should().NotBe(0,"No data in Team table");
            var request = new IdRequest { Id = id.Value };
            var response = await new TeamDetailQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }
        public TeamListRequest getValidRequest()
        {
            return new TeamListRequest();
        }
        
        [TestMethod]
        public async Task TeamListQuery_ValidRequest_IsOk()
        {
            var request = getValidRequest();
            var response = await new TeamListQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Should().NotBeEmpty();
        }
    }
}

