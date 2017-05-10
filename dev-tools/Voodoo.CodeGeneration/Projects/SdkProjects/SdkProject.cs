using System.IO;

namespace Voodoo.CodeGeneration.Projects.SdkProjects
{
    public class SdkProject : IProject
    {
        private string assemblyName;
        private Project obj;
        private string outputPath;
        private string outputType;
        private string path;
        private string projectFoleder;
        private string projectName;
        private string rootNamespace;
        private string targetFramework;

        public SdkProject(Project obj, string path)
        {
            this.obj = obj;
            this.path = path;
            projectFoleder = Path.GetDirectoryName(path);
            findProperties();
            setDefaults();
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

        public void Save()
        {
            //Do Nothing
        }

        public bool Contains(string item)
        {
            return File.Exists(item);
        }

        public void AddItem(string visualStudioItemTypeNode, string pathToProject)
        {
            //Do Nothing
        }

        public bool NeedsUpdating => false;

        private void findProperties()
        {
            foreach (var propertyGroup in obj.PropertyGroup)
            {
                if (propertyGroup.TargetFramework != null)
                    targetFramework = propertyGroup.TargetFramework;
                if (propertyGroup.AssemblyName != null)
                    assemblyName = propertyGroup.AssemblyName;
                if (propertyGroup.RootNamespace != null)
                    rootNamespace = propertyGroup.RootNamespace;
                if (propertyGroup.OutputPath != null)
                    outputPath = propertyGroup.OutputPath;
                if (propertyGroup.OutputType != null)
                    outputType = propertyGroup.OutputType;
            }
        }

        private void setDefaults()
        {
            projectName = Path.GetFileNameWithoutExtension(path);
            assemblyName = assemblyName ?? projectName;
            rootNamespace = rootNamespace ?? projectName;
            if (outputPath == null)
                outputPath = $@"bin\debug\{targetFramework}";
            outputPath = IoNic.PathCombineLocal(projectFoleder, outputPath);
            if (!outputPath.EndsWith(@"\"))
                outputPath = $@"{outputPath}\";

            outputType = outputType ?? "dll";
        }
    }
}