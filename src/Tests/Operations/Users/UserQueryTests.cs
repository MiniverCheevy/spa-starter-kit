using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Users;
using Core.Operations.Users.Extras;
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
namespace Tests.Operations.Users
{
    [TestClass]
    public class UserQueryTests
    {
        [TestMethod]
        public async Task UserDetailQuery_ValidRequest_IsOk()
        {
            int? id = 0;
            using (var context = IOC.GetContext())
            {
                if (context.Users.Any())
                id = context.Users.Max(c => c.Id);
            }
            id.Should().HaveValue().Should().NotBe(0,"No data in User table");
            var request = new IdRequest { Id = id.Value };
            var response = await new UserDetailQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }
        public UserListRequest getValidRequest()
        {
            return new UserListRequest();
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

