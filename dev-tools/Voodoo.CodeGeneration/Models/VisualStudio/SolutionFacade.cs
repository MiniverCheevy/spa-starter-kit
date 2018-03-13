using System;
using System.Collections.Generic;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Projects;

namespace Voodoo.CodeGeneration.Models.VisualStudio
{
    public class SolutionFacade
    {
        private readonly Dictionary<string, IProject> projects = new Dictionary<string, IProject>();
        public ProjectFacade PCLProject;


       

        public string SolutionName { get; set; }
        public string RootFolder { get; set; }
        public ProjectFacade ModelProject { get; set; }        
        public ProjectFacade LogicProject { get; set; }
        public ProjectFacade WebProject { get; set; }
        public ProjectFacade TestProject { get; set; }
        public ProjectFacade DataProject { get; set; }
        public Type ContextType { get; set; }
        public string[] CommandLineArguments { get; set; }
        public string JsAppPathFromWebProjectRoot { get; set; }
        public string IonicAppPathFromWebProjectRoot { get; set; }
        public bool? AddToSourceControl { get; set; }
        public string ContextTypeName { get; set; }
        public string SourceControlProviderName { get; set; }
        public WebFramework WebFramework { get; set; }
        public string PathToTfDotExe { get; set; }
        public string FullPath { get; set; }
        public string VisualStudioPath { get; set; }
        public bool WebIsAspDotNetCore { get; internal set; }

        public SolutionFacade()
        {
            if (!AddToSourceControl.HasValue)
                AddToSourceControl = true;
        }
      
    }
}