using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel
{
    public partial class DeleteCommandTemplate
    {
        public DeleteCommandFile File { get; set; }
    }

    public class DeleteCommandFile : TypedCodeFile
    {
        public DeleteCommandFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new DeleteCommandTemplate {File = this};
            Name = string.Format("{0}DeleteCommand", Name);
            PageSpecificUsingStatements.Add(ContextNamespace);
            PageSpecificUsingStatements.Add(type.Namespace);
            PageSpecificUsingStatements.Add("Voodoo.Infrastructure");
            PageSpecificUsingStatements.Add($"{Namespace}.Extras");
            PageSpecificUsingStatements.Add($"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
            UseSoftDelete = type.HasActiveFlag;
        }

        public DeleteCommandTemplate Template { get; set; }
        public bool UseSoftDelete { get; set; }

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