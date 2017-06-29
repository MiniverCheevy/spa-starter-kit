
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
    public class UserDeleteCommandTests
    {
        [TestMethod]
        public async Task UserDeleteCommand_ValidRequest_IsOk()
        {
            var model = await UserTestHelper.GetExistingUser();
            var request = new IdRequest { Id=model.Id };
            var command = new UserDeleteCommand(request);
            
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(UserMessages.DeleteOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

