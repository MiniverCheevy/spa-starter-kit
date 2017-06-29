
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
    public class MemberUpdateCommandTests
    {
        [TestMethod]
        public async Task MemberUpdateCommand_ValidRequest_IsOk()
        {
            var request = await MemberTestHelper.GetExistingMember();
            var command = new MemberSaveCommand(request);
            var response = await command.ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(MemberMessages.UpdateOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

