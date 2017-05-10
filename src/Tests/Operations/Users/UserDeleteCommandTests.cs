using System.Threading.Tasks;
using Fernweh.Core.Operations.Users;
using Fernweh.Core.Operations.Users.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;

namespace Fernweh.Tests.Operations.Users
{
    [TestClass]
    public class UserDeleteCommandTests
    {
        [TestMethod]
        public async Task UserDeleteCommand_ValidRequest_IsOk()
        {
            var model = await UserTestHelper.GetExistingUser();
            var request = new IdRequest {Id = model.Id};
            var command = new UserDeleteCommand(request);

            var response = await command.ExecuteAsync();

            response.Details.Should().BeEmpty();
            response.Message.Should().Be(UserMessages.DeleteOk);
            response.IsOk.Should().BeTrue();
        }
    }
}