using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.ProjectLevel.NameValuePairs
{
    public partial class ListsRequestTemplate
    {
        public ListsRequestFile File { get; set; }
    }

    public class ListsRequestFile : TypedCodeFile
    {
        public ListsRequestTemplate Template { get; set; }

        public ListsRequestFile(ProjectFacade project)
            : base(project, null)
        {
            Template = new ListsRequestTemplate {File = this};
            Name = "ListsRequest";
            PageSpecificUsingStatements.Add("System.Collections.Generic");
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return @"Operations\Lists";
        }
    }
}