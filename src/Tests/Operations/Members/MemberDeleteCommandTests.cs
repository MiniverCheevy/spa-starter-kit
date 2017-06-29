
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Members;
using Core.Operations.Members.Extras;
using Voodoo.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;

namespace Tests.Operations.Members
{
    [TestClass]
    public class MemberDeleteCommandTests
    {
        [TestMethod]
        public async Task MemberDeleteCommand_ValidRequest_IsOk()
        {
            var model = await MemberTestHelper.GetExistingMember();
            var request = new IdRequest { Id=model.Id };
            var command = new MemberDeleteCommand(request);
            
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(MemberMessages.DeleteOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

