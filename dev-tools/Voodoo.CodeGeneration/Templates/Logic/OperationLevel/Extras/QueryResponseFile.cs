using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public partial class QueryResponseTemplate
    {
        public QueryResponseFile File { get; set; }
    }

    public class QueryResponseFile : TypedCodeFile
    {
        public QueryResponseTemplate Template { get; set; }

        public QueryResponseFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new QueryResponseTemplate {File = this};
            Name = string.Format("{0}QueryResponse", Name);
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return string.Format(@"Operations\{0}", ExtrasFolder);
        }
    }
}