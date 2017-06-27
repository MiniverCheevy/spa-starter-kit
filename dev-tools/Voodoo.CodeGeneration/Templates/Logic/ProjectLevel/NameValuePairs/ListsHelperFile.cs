using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.ProjectLevel.NameValuePairs
{
    public partial class ListsHelperTemplate
    {
        public ListsHelperFile File { get; set; }
    }

    public class ListsHelperFile : TypedCodeFile
    {
        public ListsHelperTemplate Template { get; set; }
        public NameValuePairTypeInformation[] NameValuePairTypes { get; set; }

        public ListsHelperFile(ProjectFacade project, NameValuePairTypeInformation[] nameValuePairTypes)
            : base(project, null)
        {
            Template = new ListsHelperTemplate {File = this};
            Name = "ListsHelper";
            NameValuePairTypes = nameValuePairTypes;
            OverwriteExistingFile = true;
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add(ContextNamespace);
            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("Voodoo");
            PageSpecificUsingStatements.Add("System.Data.Entity");
            PageSpecificUsingStatements.Add("System.Threading.Tasks");
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