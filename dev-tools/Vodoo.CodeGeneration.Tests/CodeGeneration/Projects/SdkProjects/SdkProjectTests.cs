using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Voodoo;
using Voodoo.CodeGeneration.Projects.SdkProjects;
using Project = Voodoo.CodeGeneration.Projects.SdkProjects.Project;

namespace Fernweh.Tests.CodeGeneration.Projects.SdkProjects
{
    [TestClass]
    public class SdkProjectTests
    {
        private Project customizedCsProj;
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

        public SdkProjectTests()
        {
            this.customizedCsProj =
                Objectifyer.FromXml<Project>(loadResource("CodeGeneration.Projects.SdkProjects.customized.csproj.xml"));
            this.standardCsProj =
                Objectifyer.FromXml<Project>(loadResource("CodeGeneration.Projects.SdkProjects.standard.csproj.xml"));
        }

        [TestMethod]
        public void CustomizedProject_ReadNamespace_IsOk()
        {
            var project = new SdkProject(customizedCsProj, @"C:\temp\Junk.csproj");
            project.GeRootNamespace().Should().Be("Junk2");
        }

        [TestMethod]
        public void StandardProject_ReadNamespace_IsOk()
        {
            var project = new SdkProject(standardCsProj, @"C:\temp\Fernweh.Core.csproj");
            project.GeRootNamespace().Should().Be("Fernweh.Core");
        }

        [TestMethod]
        public void CustomizedProject_ReadAssemblyName_IsOk()
        {
            var project = new SdkProject(customizedCsProj, @"C:\temp\Junk.csproj");
            project.GetAssemblyName().Should().Be("Junk1");
        }

        [TestMethod]
        public void StandardProject_ReadAssemblyName_IsOk()
        {
            var project = new SdkProject(standardCsProj, @"C:\temp\Fernweh.Core.csproj");
            project.GetAssemblyName().Should().Be("Fernweh.Core");
        }

        [TestMethod]
        public void CustomizedProject_ReadOutputPath_IsOk()
        {
            var project = new SdkProject(customizedCsProj, @"C:\temp\Junk.csproj");
            project.GetOutputPath().Should().Be(@"C:\temp\bin\Debug\net46ww\");
        }

        [TestMethod]
        public void StandardProject_ReadOutputPath_IsOk()
        {
            var project = new SdkProject(standardCsProj, @"C:\temp\Fernweh.Core.csproj");
            project.GetOutputPath().Should().Be(@"C:\temp\bin\debug\net461\");
        }
    }
}