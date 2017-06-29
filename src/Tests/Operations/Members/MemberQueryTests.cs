using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Members;
using Core.Operations.Members.Extras;
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
namespace Tests.Operations.Members
{
    [TestClass]
    public class MemberQueryTests
    {
        [TestMethod]
        public async Task MemberDetailQuery_ValidRequest_IsOk()
        {
            int? id = 0;
            using (var context = IOC.GetContext())
            {
                if (context.Members.Any())
                id = context.Members.Max(c => c.Id);
            }
            id.Should().HaveValue().Should().NotBe(0,"No data in Member table");
            var request = new IdRequest { Id = id.Value };
            var response = await new MemberDetailQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }
        public MemberListRequest getValidRequest()
        {
            return new MemberListRequest();
        }
        
        [TestMethod]
        public async Task MemberListQuery_ValidRequest_IsOk()
        {
            var request = getValidRequest();
            var response = await new MemberListQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Should().NotBeEmpty();
        }
    }
}

