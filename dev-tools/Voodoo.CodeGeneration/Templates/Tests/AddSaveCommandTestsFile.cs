using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Tests
{
    public partial class AddSaveCommandTestsTemplate
    {
        public AddSaveCommandTestsFile File { get; set; }
    }

    public class AddSaveCommandTestsFile : TypedTestFile
    {
        public AddSaveCommandTestsFile(ProjectFacade project, TypeFacade type, ProjectFacade logic)
            : base(project, type)
        {
            Template = new AddSaveCommandTestsTemplate {File = this};
            Name = $"{Name}AddCommandTests";
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName + ".Extras");
            PageSpecificUsingStatements.Add("Voodoo.TestData");
        }

        public AddSaveCommandTestsTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return $@"Operations\{PluralName}";
        }
    }
}