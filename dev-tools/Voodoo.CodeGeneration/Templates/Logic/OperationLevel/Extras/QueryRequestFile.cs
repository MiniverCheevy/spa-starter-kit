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
        public QueryRequestTemplate Template { get; set; }

        public QueryRequestFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new QueryRequestTemplate {File = this};
            Name = $"{Name}QueryRequest";

            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.ComponentModel.DataAnnotations");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add("Voodoo.Messages");
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