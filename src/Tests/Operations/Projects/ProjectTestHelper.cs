
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Projects;
using Core.Operations.Projects.Extras;
using Voodoo.TestData;
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
    public class ProjectTestHelper
    {
        public static ProjectDetail GetNewProject()
        {
            var request= new ProjectDetail();
            TestHelper.Randomizer.Randomize(request);
            return request;
        }
        
        public static async Task<ProjectDetail> GetExistingProject()
        {
            var request= GetNewProject();
            var command = new ProjectSaveCommand(request);
            var response = await command.ExecuteAsync();
            
            response.Details.Should().BeEmpty();
            response.Message.Should().Be(ProjectMessages.AddOk);
            response.IsOk.Should().BeTrue();
            response.NewItemId.Should().NotBe(0);
            
            var query = new ProjectDetailQuery(new IdRequest{Id =response.NewItemId});
            var data= await query.ExecuteAsync();
            return data.Data;
        }
    }
}

