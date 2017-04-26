using System.Collections.Generic;
using System.Linq;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.ProjectLevel.NameValuePairs
{
    public partial class ListsResponseTemplate
    {
        public ListsResponseFile File { get; set; }
    }

    public class ListsResponseFile : TypedCodeFile
    {
        public ListsResponseFile(ProjectFacade project, NameValuePairTypeInformation[] nameValuePairTypes)
            : base(project, null)
        {
            Template = new ListsResponseTemplate {File = this};
            Name = "ListsResponse";
            OverwriteExistingFile = true;
            TypeNames = nameValuePairTypes.Select(c => c.PluralName).ToArray();
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
        }

        public ListsResponseTemplate Template { get; set; }
        public IEnumerable<string> TypeNames { get; set; }

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