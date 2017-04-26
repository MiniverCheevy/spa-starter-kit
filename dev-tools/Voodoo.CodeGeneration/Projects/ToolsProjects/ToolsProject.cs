using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ToolsProjectInternal = Voodoo.CodeGeneration.Projects.ToolsProjects.Project;

namespace Voodoo.CodeGeneration.Projects.ToolsProjects
{
    public class ToolsProject : IProject
    {
        private ToolsProjectInternal obj;
        private string path;
        private string assemblyName;
        private string rootNamespace;
        private string projectName;
        private string outputPath;
        private string projectFoleder;
        private string outputType;
        private List<ProjectPropertyGroup> propertyGroups;
        private List<ProjectItemGroup> itemGroups;
        private IEnumerable<object> allItems;
        private List<ProjectItemGroupCompile> itemsWithCompile;
        private List<ProjectItemGroupNone> itemsWithNone;
        private List<ProjectItemGroupContent> itemsWithContent;
        private ProjectItemGroup defaultCompileItemGroup;
        private ProjectItemGroup defaultNoneItemGroup;
        private ProjectItemGroup defaultContentItemGroup;

        public ToolsProject(ToolsProjectInternal obj, string path)
        {
            this.obj = obj;
            this.path = path;
            this.projectFoleder = Path.GetDirectoryName(path);
            propertyGroups = obj.Items.Select(c => c as ProjectPropertyGroup).Where(c => c != null).ToList();
            itemGroups = obj.Items.Select(c => c as ProjectItemGroup).Where(c => c != null).ToList();
            allItems = itemGroups.Where(c => c.Items != null).SelectMany(c => c.Items).ToList();

            foreach (var item in itemGroups)
            {
                if (defaultCompileItemGroup == null && item.Items != null && item.Items.Any(c => c is ProjectItemGroupCompile))
                {
                    defaultCompileItemGroup = item;
                }
                if (defaultNoneItemGroup == null && item.Items != null && item.Items.Any(c => c is ProjectItemGroupNone))
                {
                    defaultNoneItemGroup = item;
                }
                if (defaultContentItemGroup == null && item.Items != null && item.Items.Any(c => c is ProjectItemGroupContent))
                {
                    defaultContentItemGroup = item;
                }
            }

            //TODO: embeded resources
            itemsWithCompile = allItems.Select(c => c as ProjectItemGroupCompile).Where(c => c != null).ToList();
            itemsWithNone = allItems.Select(c => c as ProjectItemGroupNone).Where(c => c != null).ToList();
            itemsWithContent = allItems.Select(c => c as ProjectItemGroupContent).Where(c => c != null).ToList();
            findProperties();
            setDefaults();
        }

        public void Save()
        {
            var xml = Objectifyer.ToXml(obj);
            IoNic.WriteFile(xml, this.path);
        }

        private void findProperties()
        {

            foreach (var propertyGroup in propertyGroups)
            {

                if (propertyGroup.AssemblyName != null)
                    this.assemblyName = propertyGroup.AssemblyName;
                if (propertyGroup.RootNamespace != null)
                    this.rootNamespace = propertyGroup.RootNamespace;
                if (propertyGroup.OutputPath != null && propertyGroup.Condition.ToLower().Contains("debug"))
                    this.outputPath = propertyGroup.OutputPath;
                if (propertyGroup.OutputType != null)
                    this.outputType = propertyGroup.OutputType == "Library" ? "dll" : "exe";
            }
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

        private object[] AddToArray(object[] source, object item)
        {
            var result = new List<object>(source);
            result.Add(item);
            return result.ToArray();
        }
        private object locker = new object();
        public void AddItem(string visualStudioItemTypeNode, string pathToProject)
        {
            if (Contains(pathToProject))
                return;
            lock (locker)
            {
                switch (visualStudioItemTypeNode)
                {
                    case "Compile":
                        var newCompileItem = new ProjectItemGroupCompile { Include = pathToProject };
                        this.defaultCompileItemGroup.Items = AddToArray(this.defaultCompileItemGroup.Items, newCompileItem);
                        break;

                    case "Content":
                        var newContentItem = new ProjectItemGroupContent { Include = pathToProject };
                        this.defaultContentItemGroup.Items = AddToArray(this.defaultContentItemGroup.Items, newContentItem);
                        break;

                    case "EmbeddedResource":
                        //, typeof(ProjectItemGroupEmbeddedResource))]
                        break;
                    case "None":
                        var newNoneItem = new ProjectItemGroupNone { Include = pathToProject };
                        this.defaultNoneItemGroup.Items = AddToArray(this.defaultContentItemGroup.Items, newNoneItem);
                        break;
                }
            }
        }

        public bool Contains(string item)
        {

            return itemsWithCompile.Any(c => c.Include == item)
                || itemsWithNone.Any(c => c.Include == item)
                || itemsWithContent.Any(c => c.Include == item);

        }

        public bool NeedsUpdating => true;
    }
}