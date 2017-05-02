using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel
{
    public partial class AddCommandTemplate
    {
        public AddCommandFile File { get; set; }
    }

    public class AddCommandFile : TypedCodeFile
    {
        public AddCommandFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new AddCommandTemplate {File = this};
            Name = string.Format("{0}AddCommand", Name);
            PageSpecificUsingStatements.Add(ContextNamespace);
            PageSpecificUsingStatements.Add(type.Namespace);
            PageSpecificUsingStatements.Add("Voodoo.Infrastructure");
            PageSpecificUsingStatements.Add($"{Namespace}.Extras");            
            PageSpecificUsingStatements.Add($"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
        }

        public AddCommandTemplate Template { get; set; }

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