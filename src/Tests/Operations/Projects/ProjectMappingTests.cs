
using Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using Core.Operations.Projects;
using Core.Operations.Projects.Extras;
using Core.Models.Scratch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Text;
using System.Threading.Tasks;
using Voodoo;
using Voodoo.Messages;
using Voodoo.TestData;

namespace Tests.Operations.Projects
{
    [TestClass]
    public class ProjectMappingTests
    {
        private Randomizer randomizer = new Randomizer();
        
        private Project arrange()
        {
            TestHelper.SetRandomDataSeed(1);
            var source = new Project();
            TestHelper.Randomizer.Randomize(source);
            return source;
        }
        
        [TestMethod]
        public void Map_Message_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<Project, ProjectRow>();
            var source = arrange();
            var message = source.ToProjectRow();
            var target = new Project();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
            
        }
        
        [TestMethod]
        public void Map_Detail_PropertiesTheSame()
        {
            var testHelper =
            new MappingTesterHelper<Project, ProjectDetail>();
            var source = arrange();
            var message = source.ToProjectDetail();
            var target = new Project();
            target.UpdateFrom(message);
            
            testHelper.Compare(source, message, new string[]{});
            testHelper.Compare(target, message, new[] { "Id" });
            
        }
        
    }
}

