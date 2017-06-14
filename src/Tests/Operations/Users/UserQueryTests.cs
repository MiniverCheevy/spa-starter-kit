using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Operations.Users;
using Core.Operations.Users.Extras;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo.Messages;

namespace Tests.Operations.Users
{
    [TestClass]
    public class UserQueryTests
    {
        [TestMethod]
        public async Task UserDetailQuery_ValidRequest_IsOk()
        {
            int? id = null;
            using (var context = IOC.GetContext())
            {
                id = context.Users.Max(c => c.Id);
            }
            id.Should().HaveValue().Should().NotBe(0,
                "No data in User table");
            var request = new IdRequest {Id = id.Value};
            var response = await new UserDetailQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }

        public UserQueryRequest getValidRequest()
        {
            return new UserQueryRequest();
        }

        [TestMethod]
        public async Task UserListQuery_ValidRequest_IsOk()
        {
            var request = getValidRequest();
            var response = await new UserListQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Should().NotBeEmpty();
        }
    }
}