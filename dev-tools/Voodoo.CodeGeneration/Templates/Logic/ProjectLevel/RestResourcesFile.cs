using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.ProjectLevel
{
    public partial class RestResourcesTemplate
    {
        public RestResourcesFile File { get; set; }
    }

    public class RestResourcesFile : CodeFile
    {
        public RestResourcesFile(ProjectFacade project)
            : base(project)
        {
            Template = new RestResourcesTemplate {File = this};
            Name = "RestResources";
            OverwriteExistingFile = true;
        }

        public RestResourcesTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return @"";
        }
    }
}