using System;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Aurelia
{
    public partial class TypeScriptModelsTemplate
    {
        public TypeScriptModelsFile File { get; set; }
    }

    public class TypeScriptModelsFile : TypeScriptModelsFileBase
    {
        public TypeScriptModelsFile(ProjectFacade project, Type[] types, string path) : base(project, types, path)
        {
            Builder.AddTypes(types);
            Template = new TypeScriptModelsTemplate {File = this};
            OverwriteExistingFile = true;
        }

        public TypeScriptModelsTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }
    }
}