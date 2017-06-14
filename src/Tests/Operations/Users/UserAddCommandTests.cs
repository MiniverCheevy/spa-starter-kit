using System.Threading.Tasks;
using Core.Operations.Users;
using Core.Operations.Users.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fernweh.Tests.Operations.Users
{
    [TestClass]
    public class UserAddCommandTests
    {
        [TestMethod]
        public async Task UserAddCommand_ValidRequest_IsOk()
        {
            var request = UserTestHelper.GetNewUser();
            var command = new UserSaveCommand(request);

            var response = await command.ExecuteAsync();

            response.Details.Should().BeEmpty();
            response.Message.Should().Be(UserMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);
        }
    }
}