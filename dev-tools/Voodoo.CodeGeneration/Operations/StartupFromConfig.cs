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
using Voodoo.CodeGeneration.Projects;
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
            if (!string.IsNullOrWhiteSpace(configFile.SolutionName))
                solution.SolutionName = configFile.SolutionName;

            solution.ModelProject = getProject(configFile.ModelProject, true, solution.RootFolder);
            solution.LogicProject = getProject(configFile.LogicProject, true, solution.RootFolder);
            solution.WebProject = getProject(configFile.WebProject, false, solution.RootFolder);
            solution.TestProject = getProject(configFile.TestProject, false, solution.RootFolder);
            solution.DataProject = getProject(configFile.DataProject, true, solution.RootFolder);
            solution.PCLProject = getProject(configFile.PclProject, false, solution.RootFolder);

            
            solution.AddToSourceControl = configFile.AddToSourceControl;


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

        private ProjectFacade getProject(ProjectRef projectRef, bool needsAssembly, string spawnPath)
        {
            var path = projectRef?.RootPath;
            if (path == null)
                return null;

            var project = Project.GetProject(projectRef, spawnPath);            

            var key = path.ToLower();
            return projects.ContainsKey(key) ? projects[key] : new ProjectFacade(project, needsAssembly);
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
}