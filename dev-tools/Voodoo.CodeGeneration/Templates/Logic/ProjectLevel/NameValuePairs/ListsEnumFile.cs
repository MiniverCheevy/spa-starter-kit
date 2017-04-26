using System.Linq;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.ProjectLevel.NameValuePairs
{
    public partial class ListsEnumTemplate
    {
        public ListsEnumFile File { get; set; }
    }

    public class ListsEnumFile : TypedCodeFile
    {
        public ListsEnumFile(ProjectFacade project, NameValuePairTypeInformation[] nameValuePairTypes)
            : base(project, null)
        {
            Template = new ListsEnumTemplate {File = this};
            Names = nameValuePairTypes.Select(c => c.Name).ToArray();
            Name = "Lists";
            OverwriteExistingFile = true;
        }

        public ListsEnumTemplate Template { get; set; }
        public string[] Names { get; set; }

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