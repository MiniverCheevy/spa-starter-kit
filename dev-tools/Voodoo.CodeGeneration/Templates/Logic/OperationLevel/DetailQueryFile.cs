using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel
{
    public partial class DetailQueryTemplate
    {
        public DetailQueryFile File { get; set; }
    }

    public class DetailQueryFile : TypedCodeFile
    {
        public DetailQueryFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new DetailQueryTemplate {File = this};
            Name = string.Format("{0}DetailQuery", Name);
            PageSpecificUsingStatements.Add($"{Namespace}.Extras");
            PageSpecificUsingStatements.Add(type.SystemType.Namespace);
            if (HasContext)
                PageSpecificUsingStatements.Add(ContextNamespace);
        }

        public string MapMethodName { get; set; }
        public string MessageName { get; set; }
        public DetailQueryTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return string.Format(@"Operations\{0}", PluralName);
        }
    }
}