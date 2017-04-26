using System;
using System.Collections.Generic;
using System.IO;

namespace Voodoo.CodeGeneration.Projects.SdkProjects
{
    public class SdkProject : IProject
    {
        private Project obj;
        private string path;
        private string targetFramework;
        private string assemblyName;
        private string rootNamespace;
        private string projectName;
        private string outputPath;
        private string projectFoleder;
        private string outputType;

        public SdkProject(Project obj, string path)
        {
            this.obj = obj;
            this.path = path;
            this.projectFoleder = Path.GetDirectoryName(path);
            findProperties();
            setDefaults();
        }

        private void findProperties()
        {
            foreach (var propertyGroup in obj.PropertyGroup)
            {
                if (propertyGroup.TargetFramework != null)
                    this.targetFramework = propertyGroup.TargetFramework;
                if (propertyGroup.AssemblyName != null)
                    this.assemblyName = propertyGroup.AssemblyName;
                if (propertyGroup.RootNamespace != null)
                    this.rootNamespace = propertyGroup.RootNamespace;
                if (propertyGroup.OutputPath != null)
                    this.outputPath = propertyGroup.OutputPath;
                if (propertyGroup.OutputType != null)
                    this.outputType = propertyGroup.OutputType;
            }
        }

        private void setDefaults()
        {
            projectName = Path.GetFileNameWithoutExtension(path);
            this.assemblyName = this.assemblyName ?? projectName;
            this.rootNamespace = this.rootNamespace ?? projectName;
            if (outputPath == null)
                outputPath = $@"bin\debug\{this.targetFramework}";
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
    }
}