using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.TestingFramework;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo.Messages;
using Voodoo.Operations;

namespace Voodoo.CodeGeneration.Operations
{
    public class StartupFromConfig : Executor<StartupRequest, Response>
    {
        private ConfigurationFile configFile;
        private Dictionary<string, ProjectFacade> projects;
        private SolutionFacade solution;

        public StartupFromConfig(StartupRequest request) : base(request)
        {
        }

        protected override Response ProcessRequest()
        {
            var args = request.Arguments;

            if (args.Any(c => c == "debug"))
            {
                Debugger.Launch();
                args = args.ToArray().Where(c => c != "debug").ToArray();
            }

            //checkpath();
            solution = new SolutionFacade();

            Console.Write("Called from ");
            var path = request.Path;
            Console.WriteLine(path);
            Console.Write("Executing from ");
            Console.WriteLine(IoNic.GetApplicationRootDirectory());
            configFile = findConfigFile(path);

            if (configFile != null)
            {
                configureSolution();
                Vs.Helper.TestingFramework = TestingFrameworkFactory.GetFramework(configFile);
                Vs.Helper.Initialize(solution);
            }
            else
            {
                Vs.Helper.SolutionFolder = path;
            }
            Vs.Helper.SpawnPath = IoNic.GetApplicationRootDirectory();

            if (args.Length > 0 && File.Exists(args[0]))
                args = args.Except(new[] {args[0]}).ToArray();

            if (args.Any() && args[0].ToLower() == "spawn")
                args = args.Except(new[] {args[0]}).ToArray();

            CommandLineParser.ParseAndExecute(args);
            return response;
        }

        private void checkpath()
        {
            var spawnPath = IoNic.GetApplicationRootDirectory();
            var path = Environment.GetEnvironmentVariable("PATH", EnvironmentVariableTarget.User);
            if (path == null)
                throw new Exception("Environment variable PATH not set or access is denied");

            var loweredPath = path.ToLower();
            var pathParts = loweredPath.Split(';');

            if (path.ToLower().Contains(spawnPath.ToLower()))
                return;

            var newPath = $"{spawnPath};{path}";
            Environment.SetEnvironmentVariable("PATH", newPath, EnvironmentVariableTarget.User);
            Console.WriteLine("Spawn added to path");
        }

        private void configureSolution()
        {
            projects = new Dictionary<string, ProjectFacade>();

            solution.ModelProject = getProject(configFile.ModelProjectPath, true);
            solution.LogicProject = getProject(configFile.LogicProjectPath, true);
            solution.IonicProject = getProject(configFile.IonicProjectPath, false);
            solution.WebProject = getProject(configFile.WebProjectPath, false);
            solution.TestProject = getProject(configFile.TestProjectPath, false);
            solution.DataProject = getProject(configFile.DataProjectPath, true);
            solution.PCLProject = getProject(configFile.PclProjectPath, false);

            if (!string.IsNullOrWhiteSpace(configFile.SolutionName))
                solution.SolutionName = configFile.SolutionName;
            solution.AddToSourceControl = configFile.AddToSourceControl;

            if (!string.IsNullOrEmpty(configFile.PluginPath))
                Vs.Helper.PluginPath = configFile.PluginPath;

            if (solution.AddToSourceControl.To<bool>())
                solution.SourceControlProviderName = configFile.SourceControlProvider?.ToLower();
            if (!string.IsNullOrWhiteSpace(configFile.PathToTfDotExe))
                solution.PathToTfDotExe = configFile.PathToTfDotExe;

            if (!string.IsNullOrWhiteSpace(configFile.JsAppPathFromWebProjectRoot))
                solution.JsAppPathFromWebProjectRoot = configFile.JsAppPathFromWebProjectRoot;
            if (!string.IsNullOrWhiteSpace(configFile.IonicAppPathFromWebProjectRoot))
                solution.IonicAppPathFromWebProjectRoot = configFile.IonicAppPathFromWebProjectRoot;

            if (!string.IsNullOrWhiteSpace(configFile.ContextTypeName))
                solution.ContextTypeName = configFile.ContextTypeName;

            if (!string.IsNullOrWhiteSpace(configFile.WebFramework))
                solution.WebFramework = configFile.WebFramework.To<WebFramework>();

            if (!string.IsNullOrWhiteSpace(configFile.VisualStudioPath))
                solution.VisualStudioPath = configFile.VisualStudioPath;

            solution.WebIsAspDotNetCore = configFile.WebIsAspNetCore;
        }

        private ProjectFacade getProject(string path, bool needsAssembly)
        {
            if (path == null)
                return null;

            var key = path.ToLower();
            if (projects.ContainsKey(key))
                return projects[key];

            return new ProjectFacade(path, needsAssembly);
        }

        private ConfigurationFile findConfigFile(string path)
        {
            VoodooGlobalConfiguration.LogFilePath = path;
            const string fileName = "spawn.json";

            var file = IoNic.PathCombineLocal(path, fileName);
            var count = 0;

            while (!File.Exists(file))
            {
                var directory = Directory.GetParent(path);
                if (directory == null)
                    return null;
                path = directory.FullName;
                file = IoNic.PathCombineLocal(path, fileName);
                count++;
                if (count > 10)
                    return null;
            }

            var loadResponse = ActionHandler.Try(() =>
            {
                solution.RootFolder = Path.GetDirectoryName(file);
                var json = IoNic.ReadFile(file);
                configFile = JsonConvert.DeserializeObject<ConfigurationFile>(json);
            });
            if (!loadResponse.IsOk)
                Console.WriteLine(loadResponse.ToDebugString());

            if (configFile == null)
            {
                Console.WriteLine("Found but failed to load configuration file");
                return null;
            }
            solution.FullPath = IoNic.PathCombineLocal(path, configFile.SolutionName);

            Console.WriteLine("Loaded Configuration File");
            return configFile;
        }
    }

    public class StartupRequest
    {
        public string Path { get; set; }
        public string[] Arguments { get; set; }
    }
}