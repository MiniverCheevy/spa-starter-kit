namespace Voodoo.CodeGeneration.Models
{
    public class ConfigurationFile
    {
        public string SolutionName { get; set; }
        public string ModelProjectPath { get; set; }
        public string LogicProjectPath { get; set; }
        public string WebProjectPath { get; set; }
        public string IonicProjectPath { get; set; }
        public string TestProjectPath { get; set; }
        public string DataProjectPath { get; set; }
        public string PclProjectPath { get; set; }
        public string PluginPath { get; set; }

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