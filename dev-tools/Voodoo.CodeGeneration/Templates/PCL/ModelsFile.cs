using System;
using Voodoo.CodeGeneration.Helpers.ModelBuilders;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.PCL
{
    public partial class ModelsTemplate
    {
        public ModelsFile File { get; set; }
    }

    public class ModelsFile : CodeFile
    {
        public override string VisualStudioItemTypeNode => "Compile";
        public PclGraphBuilder Builder { get; set; } = new PclGraphBuilder(null);
        public override string FileName => "models.generated.cs";
        public ModelsTemplate Template { get; set; }

        public override string Namespace => "Shared";

        public ModelsFile(ProjectFacade project, Type[] types) : base(project)
        {
            Builder = new PclGraphBuilder(types);
            Builder.WriteModelDefinitions();

            Template = new ModelsTemplate {File = this};
            OverwriteExistingFile = true;
        }

        public override string GetFolder()
        {
            return @"";
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }
    }
}