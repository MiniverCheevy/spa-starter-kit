using System.Linq;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public partial class RepositoryTemplate
    {
        public RepositoryFile File { get; set; }
    }

    public class RepositoryFile : TypedCodeFile
    {
        public string FriendlyName { get; set; }
        public RepositoryTemplate Template { get; set; }
        public string[] Errors { get; set; }

        public RepositoryFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            FriendlyName = Name.ToFriendlyString();
            Template = new RepositoryTemplate {File = this};
            Name = string.Format("{0}Repository", Name);
            PageSpecificUsingStatements.Add(Type.SystemType.Namespace);
            


            Errors = type.Properties.SelectMany(c => c.ErrorMessages).Select(c => c.Text).ToArray();
            if (HasContext)
            {
                PageSpecificUsingStatements.Add(ContextNamespace);
                PageSpecificUsingStatements.Add("System.Data.Entity");
            }
                


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