using Voodoo.CodeGeneration.Helpers.ModelBuilders;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Angular
{
    public partial class TsServiceTemplate
    {
        public TsServiceFile File { get; set; }
    }

    public class TsServiceFile : TsServiceFileBase
    {
        public TsServiceTemplate Template { get; set; }

        public TsServiceFile(ProjectFacade project, Resource resource, string path)
            : base(project, resource, path)
        {
            Name = $"{resource.Name}Service";
            TsName = $"{ModelBuilder.lowerCaseStartingCapitalLetters(resource.Name)}Service";
            Template = new TsServiceTemplate {File = this};
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }
    }
}