using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.CodeGeneration.Projects;
using Voodoo.CodeGeneration.Projects.SdkProjects;
using Voodoo.CodeGeneration.Projects.ToolsProjects;
using SdkProjectInternal = Voodoo.CodeGeneration.Projects.SdkProjects.Project;
using ToolsProjectInternal = Microsoft.Build.Evaluation.Project;

namespace Voodoo.CodeGeneration.Projects
{
    public class ProjectHelper
    {
        public static IProject GetProject(string path)
        {
            var xml = IoNic.ReadFile(path);
            if (xml.Contains("Microsoft.NET.Sdk"))
            {
                var obj = Objectifyer.FromXml<SdkProjectInternal>(xml);
                return new SdkProject(obj, path);
            }
            else
            {
                var obj = new ToolsProjectInternal(path);
                return new ToolsProject(obj, path);
            }
        }
    }
}