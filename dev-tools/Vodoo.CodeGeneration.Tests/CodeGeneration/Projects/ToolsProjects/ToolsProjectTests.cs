using System.IO;
using Fernweh.Tests.CodeGeneration.Projects.SdkProjects;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo;
using Voodoo.CodeGeneration.Projects.ToolsProjects;

namespace Fernweh.Tests.CodeGeneration.Projects.ToolsProjects
{
    [TestClass]
    public class ToolsProjectTests
    {
        private Project standardCsProj;

        private string loadResource(string name)
        {
            var assembly = typeof(SdkProjectTests).Assembly;
            name = $"{assembly.GetName().Name}.{name}";
            using (var stream = assembly.GetManifestResourceStream(name))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public ToolsProjectTests()
        {
            this.standardCsProj =
                Objectifyer.FromXml<Project>(loadResource("CodeGeneration.Projects.ToolsProjects.standard.csproj.xml"));
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