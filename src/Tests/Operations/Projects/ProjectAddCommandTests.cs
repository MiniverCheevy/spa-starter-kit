
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Projects;
using Core.Operations.Projects.Extras;
using Voodoo.TestData;
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
    public class ProjectAddCommandTests
    {
        [TestMethod]
        public async Task ProjectAddCommand_ValidRequest_IsOk()
        {
            var request = ProjectTestHelper.GetNewProject();
            var command = new ProjectSaveCommand(request);
            
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ProjectMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);
        }
    }
}

