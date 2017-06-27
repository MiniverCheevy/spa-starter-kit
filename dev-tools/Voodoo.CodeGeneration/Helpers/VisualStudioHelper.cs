using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Voodoo.CodeGeneration.Infrastructure;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.SourceControl;
using Voodoo.CodeGeneration.Models.TestingFramework;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.Logging;

namespace Voodoo.CodeGeneration.Helpers
{
    public class VisualStudioHelper
    {
        public List<LogEntry> Log { get; set; }
        public SolutionFacade Solution { get; set; }
        public List<ProjectFacade> Projects { get; set; }
        public string GenFolder { get; set; }
        public string SolutionFolder { get; set; }
        public List<ScratchFile> ScratchFiles { get; set; }
        public bool HasConfig { get; set; }
        public ITestingFramework TestingFramework { get; set; }
        public string SpawnPath { get; set; }
        public string PluginPath { get; set; }
        public Flags Flags { get; set; } = new Flags();

        internal VisualStudioHelper()
        {
            HasConfig = false;
            ScratchFiles = new List<ScratchFile>();
            Projects = new List<ProjectFacade>();
            Log = new List<LogEntry>();
            Flags = new Flags();
        }


        public VisualStudioHelper(SolutionFacade solution) : this()
        {
            Solution = solution;
            HasConfig = true;
        }
        public string[] VisualStudioVersions { get; internal set; } =  new string [] {"9.0", "10.0", "11.0", "12.0", "14.0", "15.0"};

        public TypeFacade FindType(string typeName)
        {
            foreach (var project in Projects)
            {
                var type = project.FindType(typeName);
                if (type != null)
                    return new TypeFacade(type);
            }
            return null;
        }

        public void Initialize(SolutionFacade solution)
        {
            Solution = solution;
            HasConfig = true;
            UnloadAllProjects();
            findSolutionPath();
            if (Solution.IonicProject != null)
                Projects.Add(Solution.IonicProject);
            if (Solution.WebProject != null)
                Projects.Add(Solution.WebProject);
            if (Solution.LogicProject != null)
                Projects.Add(Solution.LogicProject);
            if (Solution.ModelProject != null)
                Projects.Add(Solution.ModelProject);
            if (Solution.TestProject != null)
                Projects.Add(Solution.TestProject);
            if (Solution.DataProject != null)
            {
                Projects.Add(Solution.DataProject);
                if (Solution.PCLProject != null)
                    Projects.Add(Solution.PCLProject);
            }
            Projects.ForEach(c => c.Initialize());

            var provider = SourceControlProviderFactory.GetProvider();
            if (provider != null)
            {
                var paths = Projects.Select(c => c.FullPath).Distinct().ToArray();
                foreach (var path in paths)
                    provider.CheckOutFiles(path);
            }

            findContext();
        }

        private void findContext()
        {
            if (!string.IsNullOrWhiteSpace(Solution.ContextTypeName))
                try
                {
                    foreach (var project in Projects)
                    {
                        var type = project.FindType(Solution.ContextTypeName);
                        if (type != null)
                        {
                            Solution.ContextType = type;
                            return;
                        }
                    }
                }
                catch
                {
                    Vs.Helper.Log.Add(new LogEntry
                    {
                        Level = LogLevels.Error,
                        Message = $"Could not find Context: {Solution.ContextTypeName}"
                    });
                }
        }

        private void findSolutionPath()
        {
            GenFolder = Solution.RootFolder;
            if (string.IsNullOrEmpty(GenFolder))
                GenFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (GenFolder == null)
                throw new Exception("Could not location starting directory");

            VoodooGlobalConfiguration.LogFilePath = GenFolder;
            var path = GenFolder;
            if (string.IsNullOrWhiteSpace(Solution.SolutionName))
                throw new Exception("Solution not set");
            var solutionFile = IoNic.PathCombineLocal(path, Solution.SolutionName);
            var count = 0;

            while (!File.Exists(solutionFile))
            {
                var directory = Directory.GetParent(path);
                if (directory == null)
                    throw new Exception("Cannot find solution");
                path = directory.FullName;
                solutionFile = IoNic.PathCombineLocal(path, Solution.SolutionName);
                count++;
                if (count > 10)
                    throw new Exception("Cannot find solution");
            }
            SolutionFolder = Path.GetDirectoryName(solutionFile) + @"\";
        }

        internal void UnloadAllProjects()
        {
            //ProjectCollection.GlobalProjectCollection.UnloadAllProjects();
        }

        internal void WriteScratchFiles()
        {
            var files = ScratchFiles.Distinct(new CodeFileComparer()).ToList();

            foreach (var file in files)
            {
                var text = file.GetFileContents();
                IoNic.WriteFile(text, file.FullPath);
                VisualStudioAutomator.OpenFileInVisualStudio(file);
            }
        }
    }
}