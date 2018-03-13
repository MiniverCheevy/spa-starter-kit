namespace Voodoo.CodeGeneration.Models
{
    public class ProjectRef
    {
        public string RootPath { get; set; }
        public string DllPath { get; set; }
        public string Namespace { get; set; }
    }
    public class ConfigurationFile
    {
        public string SolutionName { get; set; }
        public ProjectRef ModelProject { get; set; }
        public ProjectRef LogicProject { get; set; }
        public ProjectRef WebProject { get; set; }
        public ProjectRef TestProject { get; set; }
        public ProjectRef DataProject { get; set; }
        public ProjectRef PclProject { get; set; }
        
        public string ContextTypeName { get; set; }
        public string JsAppPathFromWebProjectRoot { get; set; }
        public string IonicAppPathFromWebProjectRoot { get; set; }

        public string SourceControlProvider { get; set; }
        public string PathToTfDotExe { get; set; }
        public string TestingFramework { get; set; }
        public bool AddToSourceControl { get; set; }

        public string WebFramework { get; set; }

        public bool IonicUseTypeScript { get; set; }

        public string VisualStudioPath { get; set; }

        public bool WebIsAspNetCore { get; set; }
    }
}