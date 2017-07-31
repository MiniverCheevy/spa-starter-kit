
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
    public class ProjectUpdateCommandTests
    {
        [TestMethod]
        public async Task ProjectUpdateCommand_ValidRequest_IsOk()
        {
            var request = await ProjectTestHelper.GetExistingProject();
            var command = new ProjectSaveCommand(request);
            var response = await command.ExecuteAsync();
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ProjectMessages.UpdateOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

