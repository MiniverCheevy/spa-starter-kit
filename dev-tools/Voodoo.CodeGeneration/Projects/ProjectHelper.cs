//using Voodoo.CodeGeneration.Projects.SdkProjects;
//using Voodoo.CodeGeneration.Projects.ToolsProjects;
//using SdkProjectInternal = Voodoo.CodeGeneration.Projects.SdkProjects.Project;
//using ToolsProjectInternal = Microsoft.Build.Evaluation.Project;

using System.IO;
using Voodoo.CodeGeneration.Models;

namespace Voodoo.CodeGeneration.Projects
{

    public class Project: IProject
    {
        public bool NeedsUpdating { get; }
        public ProjectRef ProjectRef { get; private set; }
        public string AssemblyPath { get; private set; }
        public string ProjectPath { get; private set; }
        public string Namespace { get; private set; }

        public string GetFullAsseblyPath()
        {
            return AssemblyPath;
        }
       
        public string GeRootNamespace()
        {
            return Namespace ?? GetAssemblyName() ?? new DirectoryInfo(ProjectPath).Name;


        }


        public string GetAssemblyName()
        {
            return this.AssemblyPath != null ? Path.GetFileNameWithoutExtension(this.AssemblyPath) : null;
        }
      

        public bool Contains(string item)
        {
            return File.Exists(item);
        }
        public string GetFolder()
        {
            return ProjectPath;
        }
        public static IProject GetProject(ProjectRef project, string projectRoot)
        {
            var newProject = new Project
            {
                ProjectRef = project,
                AssemblyPath = project.DllPath != null ? IoNic.PathCombineLocal(projectRoot, project.RootPath, project.DllPath) : null,
                ProjectPath = IoNic.PathCombineLocal(projectRoot, project.RootPath),
                Namespace = project.Namespace
        };
            return newProject;
        }

        public IProject GetProject(ProjectRef project)
        {
            throw new System.NotImplementedException();
        }
    }
   
}