using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Aurelia
{
    public partial class TsServiceBatchTemplate
    {
        public TsServiceBatchFile File { get; set; }
    }

    public class TsServiceBatchFile : TypeScriptServiceBatchFileBase
    {
        public override string FileName => "api.generated.ts";

        public TsServiceBatchTemplate Template { get; set; }

        public TsServiceBatchFile(ProjectFacade project, Resource[] resources, string path)
            : base(project, resources, path)
        {
            Template = new TsServiceBatchTemplate {File = this};
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }
    }
}