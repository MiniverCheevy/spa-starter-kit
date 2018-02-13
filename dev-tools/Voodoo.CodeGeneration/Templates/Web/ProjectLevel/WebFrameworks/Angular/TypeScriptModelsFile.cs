using System;
using Voodoo.CodeGeneration.Helpers.ModelBuilders;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Web.ProjectLevel.WebFrameworks.Angular
{
    public partial class TypeScriptModelsTemplate
    {
        public TypeScriptModelsFile File { get; set; }
    }

    public class TypeScriptModelsFile : TypeScriptModelsFileBase
    {
        public TypeScriptModelsTemplate Template { get; set; }

        public TypeScriptModelsFile(ProjectFacade project, Type[] types, string path) : base(project, types, path)
        {
            Builder = new TypeScriptGraphBuilder(types);
            Builder.WriteModelDefinitions();

            Template = new TypeScriptModelsTemplate {File = this};
            OverwriteExistingFile = true;
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }
    }
}