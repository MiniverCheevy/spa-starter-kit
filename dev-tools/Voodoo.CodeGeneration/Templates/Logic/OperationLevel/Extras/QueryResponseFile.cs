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
            Name = $"{Name}QueryResponse";



            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add("System.Text");
            PageSpecificUsingStatements.Add("System.Threading.Tasks");
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            PageSpecificUsingStatements.Add("Voodoo.Operations.Async");
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