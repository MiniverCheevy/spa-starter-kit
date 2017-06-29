
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Users;
using Core.Operations.Users.Extras;
using Voodoo.TestData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;

namespace Tests.Operations.Users
{
    [TestClass]
    public class UserUpdateCommandTests
    {
        [TestMethod]
        public async Task UserUpdateCommand_ValidRequest_IsOk()
        {
            var request = await UserTestHelper.GetExistingUser();
            var command = new UserSaveCommand(request);
            var response = await command.ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(UserMessages.UpdateOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

