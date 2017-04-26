using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel
{
    public partial class ListQueryTemplate
    {
        public ListQueryFile File { get; set; }
    }

    public class ListQueryFile : TypedCodeFile
    {
        public ListQueryFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new ListQueryTemplate {File = this};
            Name = $"{Name}ListQuery";
            if (HasContext)
            {
                PageSpecificUsingStatements.Add(ContextNamespace);
                PageSpecificUsingStatements.Add("Voodoo");
                PageSpecificUsingStatements.Add($"{Namespace}.Extras");
            }
        }

        public ListQueryTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return $@"Operations\{PluralName}";
        }
    }
}