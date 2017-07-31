using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Projects;
using Core.Operations.Projects.Extras;
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
namespace Tests.Operations.Projects
{
    [TestClass]
    public class ProjectQueryTests
    {
        [TestMethod]
        public async Task ProjectDetailQuery_ValidRequest_IsOk()
        {
            int? id = 0;
            using (var context = IOC.GetContext())
            {
                if (context.Projects.Any())
                id = context.Projects.Max(c => c.Id);
            }
            id.Should().HaveValue().Should().NotBe(0,"No data in Project table");
            var request = new IdRequest { Id = id.Value };
            var response = await new ProjectDetailQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
        }
        public ProjectListRequest getValidRequest()
        {
            return new ProjectListRequest();
        }
        
        [TestMethod]
        public async Task ProjectListQuery_ValidRequest_IsOk()
        {
            var request = getValidRequest();
            var response = await new ProjectListQuery(request).ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().BeNull();
            response.IsOk.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Should().NotBeEmpty();
        }
    }
}

