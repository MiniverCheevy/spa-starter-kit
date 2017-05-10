using System.Threading.Tasks;
using Fernweh.Core.Operations.Lists;
using Fernweh.Core.Operations.Users;
using Fernweh.Core.Operations.Users.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;
using Voodoo.TestData;

namespace Fernweh.Tests.Operations.Users
{
    [TestClass]
    public class UserTestHelper
    {
        public static UserDetail GetNewUser()
        {
            var request = new UserDetail();
            TestHelper.Randomizer.Randomize(request);
            request.ConfirmPassword = request.Password;
            request.Roles.Add(new ListItem {Value = 1, Name = "Administrator"});
            return request;
        }

        public static async Task<UserDetail> GetExistingUser()
        {
            var request = GetNewUser();
            var command = new UserSaveCommand(request);
            var response = await command.ExecuteAsync();

            response.Details.Should().BeEmpty();
            response.Message.Should().Be(UserMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);

            var query = new UserDetailQuery(new IdRequest {Id = response.NewItemId});
            var data = await query.ExecuteAsync();
            return data.Data;
        }
    }
}