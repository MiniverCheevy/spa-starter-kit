using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Models.SourceControl;
using Voodoo.CodeGeneration.Projects;
using Voodoo.Infrastructure;
using Voodoo.Infrastructure.Notations;
using Voodoo.Logging;
using Voodoo.Messages;

namespace Voodoo.CodeGeneration.Models.VisualStudio
{
    public class ProjectFacade
    {
        private readonly List<string> filesToAddToSourceControl = new List<string>();
        private string assemblyPath;
        private bool isInitialized;
        private ISourceControlProvider sourceControl;

        public string CsProj { get; set; }
        public string RootNamespace { get; set; }
        public string Folder { get; set; }
        public string FullPath { get; set; }
        public Assembly Assembly { get; set; }
        public IProject Project { get; set; }
        public List<CodeFile> Files { get; set; }
        public List<string> UsingStatements { get; set; }
        public List<string> RestResources { get; set; }
        public List<Type> AllTypes { get; set; } = new List<Type>();
        public List<Type> ClientTypes { get; set; }

        public List<Type> MappingTypes { get; set; }

        public bool IsAssemblyLoaded { get; set; }

        public bool NeedsAssembly { get; set; }

        public ProjectFacade(string csProj, bool needsAssembly)
        {
            NeedsAssembly = needsAssembly;
            CsProj = csProj;
            UsingStatements = new List<string>();
            Files = new List<CodeFile>();
        }


        public void AddRestResource(string name)
        {
            if (!RestResources.Contains(name))
                RestResources.Add(name);
        }

        public override string ToString()
        {
            return $"{Assembly.FullName} {CsProj}";
        }

        private void discoverTypes()
        {
            AllTypes = Assembly.GetTypesSafetly().Where(c => c != null).ToList();

            ClientTypes = AllTypes.Where(c => c.GetCustomAttribute<ClientAttribute>() != null)
                .ToList();

            MappingTypes = AllTypes.Where(c => c.GetCustomAttribute<MapsToAttribute>() != null)
                .ToList();

            RestResources = AllTypes
                .Where(a => a?.CustomAttributes != null)
                .SelectMany(a => a.CustomAttributes)
                .Where(c => c.AttributeType == typeof(RestAttribute))
                .SelectMany(c => c.ConstructorArguments)
                .Where(c => c.ArgumentType == typeof(string))
                .Distinct()
                .OrderBy(c => c.Value)
                .Select(c => c.Value.ToString())
                .Distinct()
                .ToList();
        }

        public void Initialize()
        {
            if (isInitialized)
                return;

            sourceControl = SourceControlProviderFactory.GetProvider();
            FullPath = $"{Vs.Helper.SolutionFolder}{CsProj}";
            Project = Vs.Helper.Solution.GetProject(FullPath);
            Folder = Path.GetDirectoryName(FullPath);
            RootNamespace = Project.GeRootNamespace();
            UsingStatements.Add(RootNamespace);

            if (!NeedsAssembly)
            {
                isInitialized = true;
                return;
            }
            assemblyPath = getAssemblyPath();

            try
            {
                var assemblyBytes = File.ReadAllBytes(assemblyPath);
                AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += CurrentDomain_ReflectionOnlyAssemblyResolve;
                var asm = Assembly.LoadFrom(assemblyPath);
                Assembly = asm;
                discoverTypes();
                RootNamespace = Assembly.GetName().Name;
                UsingStatements.Add(RootNamespace);
                IsAssemblyLoaded = true;
                isInitialized = true;
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                IsAssemblyLoaded = false;
            }
        }

        private Assembly CurrentDomain_ReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(w => w.FullName == args.Name);
            if (assembly != null)
                return assembly;

            var name = args.Name.Split(',').FirstOrDefault().To<string>().ToLower();

            var directory = Path.GetDirectoryName(assemblyPath);
            foreach (var file in Directory.GetFiles(directory))
                if (file.ToLower().Contains(name))
                {
                    var asm = Assembly.LoadFrom(file);
                    if (asm.FullName == args.Name)
                        return asm;
                }

            return null;
        }

        private string getAssemblyPath()
        {
            if (!NeedsAssembly)
                return null;

            var outputPath = Project.GetOutputPath();
            var extension = Project.GetOutputType().ToLower() == "exe" ? "exe" : "dll";
            var assemblyName = Project.GetAssemblyName() + "." + extension;
            var assemblyDirectory = IoNic.PathCombineLocal(Path.GetDirectoryName(FullPath), outputPath);
            var asmPath = IoNic.PathCombineLocal(assemblyDirectory, assemblyName);

            if (!File.Exists(asmPath))
                throw new Exception("Try building your project (in debug mode).  Could not find " + asmPath);
            return asmPath;
        }

        public void AddFile(CodeFile file)
        {
            Files.Add(file);
        }

        public bool Contains(string item)
        {
            return Project.Contains(item);
        }

        internal void WriteFiles()
        {
            Files = Files.Distinct(new CodeFileComparer()).ToList();
            Parallel.ForEach(Files, writeFile);
            Project.Save();
            if (Vs.Helper.Solution.AddToSourceControl.To<bool>())
                addToSourceControl();
        }

        private void writeFile(CodeFile file)
        {
            if (shouldSkipFile(file))
                return;

            var text = CodeFormatter.Format(file.GetFileContents());
            var writeFile = true;
            if (File.Exists(file.FullPath))
            {
                var existingText = File.ReadAllText(file.FullPath);
                if (text == existingText)
                {
                    Vs.Helper.Log.Add(LogEntry.Info("no changes {0}", file.PathToProject));
                    writeFile = false;
                }
                else
                {
                    checkOutFiles(file.FullPath);
                }
            }

            if (!writeFile)
                return;

            writeFileToFileSystem(file, text);

            var pathToProject = Project.NeedsUpdating ? file.PathToProject : file.FullPath;

            if (Contains(pathToProject))
                return;

            var added = new Response();
            added = ActionHandler.Execute(() =>
            {
                Project.AddItem(file.VisualStudioItemTypeNode, pathToProject);
                return new Response();
            });
            if (!added.IsOk)
                LogEntry.Info("There was a problem adding '{0}' please try again");

            filesToAddToSourceControl.Add(file.FullPath);
        }

        private bool shouldSkipFile(CodeFile file)
        {
            var response = File.Exists(file.FullPath) && !file.OverwriteExistingFile;
            if (response)
                Vs.Helper.Log.Add(LogEntry.Trace(
                    "SKIPPED {0} already exists",
                    file.PathToProject));
            return response;
        }

        private static void writeFileToFileSystem(CodeFile file, string text)
        {
            IoNic.WriteFile(text, file.FullPath);
            Vs.Helper.Log.Add(LogEntry.Info("generated {0}", file.PathToProject));
        }

        private void checkOutFiles(params string[] files)
        {
            sourceControl?.CheckOutFiles(files);
        }

        private void addToSourceControl()
        {
            sourceControl?.AddFiles(filesToAddToSourceControl.ToArray());
        }

        public Type FindType(string targetTypeName)
        {
            Type type = null;
            var types =
                AllTypes.Where(
                        c =>
                            string.Equals(c.FullName, targetTypeName, StringComparison.CurrentCultureIgnoreCase) ||
                            string.Equals(c.Name, targetTypeName, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();
            if (types.Count() == 1)
                type = types.First();
            else if (types.Count > 1)
                throw new Exception(
                    $"More than one type with the name {targetTypeName} in {CsProj} project, try using the full type name");
            return type;
        }
    }
}