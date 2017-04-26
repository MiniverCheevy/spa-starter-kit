using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Helpers.ModelBuilders;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Angular2
{
    public partial class TsServiceTemplate
    {
        public TsServiceFile File { get; set; }
    }

    public class TsServiceFile : TsServiceFileBase
    {
        public TsServiceFile(ProjectFacade project, Resource resource, string path)
            : base(project, resource, path)
        {
            Name = $"{resource.Name}Service";
            TsName = $"{TypeScriptModelBuilder.LowerCaseFirstLetter(resource.Name)}Service";
            Template = new TsServiceTemplate {File = this};
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public TsServiceTemplate Template { get; set; }
    }
}