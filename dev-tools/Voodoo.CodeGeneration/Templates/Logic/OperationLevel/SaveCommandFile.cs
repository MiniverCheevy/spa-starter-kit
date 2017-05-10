using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel
{
    public partial class SaveCommandTemplate
    {
        public SaveCommandFile File { get; set; }
    }

    public class SaveCommandFile : TypedCodeFile
    {
        public SaveCommandTemplate Template { get; set; }

        public SaveCommandFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new SaveCommandTemplate {File = this};
            Name = $"{Name}SaveCommand";
            PageSpecificUsingStatements.Add(ContextNamespace);
            PageSpecificUsingStatements.Add(type.Namespace);
            PageSpecificUsingStatements.Add("Voodoo.Infrastructure");
            PageSpecificUsingStatements.Add($"{Namespace}.Extras");
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
        }


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