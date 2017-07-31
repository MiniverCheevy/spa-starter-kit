
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
    public class ProjectDeleteCommandTests
    {
        [TestMethod]
        public async Task ProjectDeleteCommand_ValidRequest_IsOk()
        {
            var model = await ProjectTestHelper.GetExistingProject();
            var request = new IdRequest { Id=model.Id };
            var command = new ProjectDeleteCommand(request);
            
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ProjectMessages.DeleteOk);
            response.IsOk.Should().BeTrue();
        }
        
    }
}

