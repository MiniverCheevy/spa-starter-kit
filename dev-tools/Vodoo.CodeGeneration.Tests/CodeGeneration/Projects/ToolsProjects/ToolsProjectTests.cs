using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using FluentAssertions;
using Microsoft.Build.Evaluation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo;
using Voodoo.CodeGeneration.Projects.ToolsProjects;
using Voodoo.CodeGeneration.Tests.CodeGeneration.Projects.SdkProjects;

namespace Voodoo.CodeGeneration.Tests.CodeGeneration.Projects.ToolsProjects
{
    [TestClass]
    public class ToolsProjectTests
    {
        private Project standardCsProj;
        private XmlReader reader;        

        ~ToolsProjectTests()
        {
            
            reader.Dispose();   
        }

        private XmlReader loadResource(string name)
        {
            var assembly = typeof(SdkProjectTests).Assembly;
            name = $"{assembly.GetName().Name}.{name}";
            var stream = assembly.GetManifestResourceStream(name);
            return XmlReader.Create(stream);


        }

        public ToolsProjectTests()
        {

            this.reader = loadResource("CodeGeneration.Projects.ToolsProjects.standard.csproj.xml");
            this.standardCsProj = new Project(reader);

        }


        [TestMethod]
        public void StandardProject_ReadNamespace_IsOk()
        {
            var project = new ToolsProject(standardCsProj, @"C:\temp\Fernweh.Core.csproj");
            project.GeRootNamespace().Should().Be("Fernweh.Core");
        }


        [TestMethod]
        public void StandardProject_ReadAssemblyName_IsOk()
        {
            var project = new ToolsProject(standardCsProj, @"C:\temp\Fernweh.Core.csproj");
            project.GetAssemblyName().Should().Be("Fernweh.Core");
        }

        [TestMethod]
        public void StandardProject_ReadOutputPath_IsOk()
        {
            var project = new ToolsProject(standardCsProj, @"C:\temp\Fernweh.Core.csproj");
            project.GetOutputPath().ToLower().Should().Be(@"c:\temp\bin\debug\");
        }

        [TestMethod]
        public void StandardProject_ReadOutputType_IsOk()
        {
            var project = new ToolsProject(standardCsProj, @"C:\temp\Fernweh.Core.csproj");
            project.GetOutputType().Should().Be("dll");

        }

        [TestMethod]
        public void StandardProject_Contains_IsOk()
        {
            var file = @"Context\FernwehContext.cs";
            var project = new ToolsProject(standardCsProj, @"C:\temp\Fernweh.Core.csproj");
            project.Contains(file).Should().BeTrue();

        }
    }
}