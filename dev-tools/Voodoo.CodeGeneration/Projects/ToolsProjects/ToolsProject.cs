using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;


namespace Voodoo.CodeGeneration.Projects.ToolsProjects
{
    public partial class ToolsProject : IProject
    {
        private Project obj;
        private string path;
        private string assemblyName;
        private string rootNamespace;
        private string projectName;
        private string outputPath;
        private string projectFoleder;
        private string outputType;

        public ToolsProject(Project obj, string path)
        {
            this.obj = obj;
            this.path = path;
            this.projectFoleder = Path.GetDirectoryName(path);
            findProperties();
            setDefaults();
        }


        public void Save()
        {
            obj.Save();
        }

        private void findProperties()
        {
            var outputPath = obj.GetProperty("OutputPath").EvaluatedValue;
            var extension = obj.GetProperty("OutputType").EvaluatedValue.ToLower() == "exe" ? "exe" : "dll";
            this.assemblyName = obj.GetProperty("AssemblyName").EvaluatedValue;
            var asmPath = IoNic.PathCombineLocal(Path.GetDirectoryName(path), outputPath, assemblyName);

        }

        private void setDefaults()
        {
            projectName = Path.GetFileNameWithoutExtension(path);
            this.assemblyName = this.assemblyName ?? projectName;
            this.rootNamespace = this.rootNamespace ?? projectName;
            if (outputPath == null)
                outputPath = $@"bin\debug\";
            outputPath = IoNic.PathCombineLocal(projectFoleder, outputPath);
            if (!outputPath.EndsWith(@"\"))
                outputPath = $@"{outputPath}\";

            this.outputType = this.outputType ?? "dll";
        }



        public string GeRootNamespace()
        {
            return rootNamespace;
        }

        public string GetOutputPath()
        {
            return outputPath;
        }

        public string GetOutputType()
        {
            return outputType;
        }

        public string GetAssemblyName()
        {
            return assemblyName;
        }

        public void AddItem(string visualStudioItemTypeNode, string pathToProject)
        {
            if (!Contains(pathToProject))
                obj.AddItem(visualStudioItemTypeNode, pathToProject);

        }

        public bool Contains(string item)
        {
            return obj.GetItemsByEvaluatedInclude(item).Any();
        }

        public bool NeedsUpdating => true;
    }
}