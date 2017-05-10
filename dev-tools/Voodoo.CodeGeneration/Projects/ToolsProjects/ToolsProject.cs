using System.IO;
using System.Linq;
using Microsoft.Build.Evaluation;

namespace Voodoo.CodeGeneration.Projects.ToolsProjects
{
    public class ToolsProject : IProject
    {
        private string assemblyName;
        private Project obj;
        private string outputPath;
        private string outputType;
        private string path;
        private string projectFoleder;
        private string projectName;
        private string rootNamespace;

        public ToolsProject(Project obj, string path)
        {
            this.obj = obj;
            this.path = path;
            projectFoleder = Path.GetDirectoryName(path);
            findProperties();
            setDefaults();
        }


        public void Save()
        {
            obj.Save();
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

        private void findProperties()
        {
            var outputPath = obj.GetProperty("OutputPath").EvaluatedValue;
            var extension = obj.GetProperty("OutputType").EvaluatedValue.ToLower() == "exe" ? "exe" : "dll";
            assemblyName = obj.GetProperty("AssemblyName").EvaluatedValue;
            var asmPath = IoNic.PathCombineLocal(Path.GetDirectoryName(path), outputPath, assemblyName);
        }

        private void setDefaults()
        {
            projectName = Path.GetFileNameWithoutExtension(path);
            assemblyName = assemblyName ?? projectName;
            rootNamespace = rootNamespace ?? projectName;
            if (outputPath == null)
                outputPath = $@"bin\debug\";
            outputPath = IoNic.PathCombineLocal(projectFoleder, outputPath);
            if (!outputPath.EndsWith(@"\"))
                outputPath = $@"{outputPath}\";

            outputType = outputType ?? "dll";
        }
    }
}