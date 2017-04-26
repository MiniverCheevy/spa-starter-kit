using System.Threading.Tasks;
using Fernweh.Core.Operations.Users;
using Fernweh.Core.Operations.Users.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fernweh.Tests.Operations.Users
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

