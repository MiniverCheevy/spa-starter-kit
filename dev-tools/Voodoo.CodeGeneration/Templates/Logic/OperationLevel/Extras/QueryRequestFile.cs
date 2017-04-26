using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public partial class QueryRequestTemplate
    {
        public QueryRequestFile File { get; set; }
    }

    public class QueryRequestFile : TypedCodeFile
    {
        public QueryRequestFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new QueryRequestTemplate {File = this};
            Name = string.Format("{0}QueryRequest", Name);
        }

        public QueryRequestTemplate Template { get; set; }

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