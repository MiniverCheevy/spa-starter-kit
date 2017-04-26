using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Tests
{
    public partial class TestHelperSaveTemplate
    {
        public TestHelperSaveFile File { get; set; }
    }

    public class TestHelperSaveFile : TypedTestFile
    {
        public TestHelperSaveFile(ProjectFacade project, TypeFacade type, ProjectFacade logic)
            : base(project, type)
        {
            Template = new TestHelperSaveTemplate {File = this};
            Name = $"{Name}TestHelper";
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName + ".Extras");
            PageSpecificUsingStatements.Add("Voodoo.TestData");
            PageSpecificUsingStatements.Add(ContextNamespace);
            PageSpecificUsingStatements.Add(logic.RootNamespace);
        }

        public Operation Operation { get; set; }

        public ProjectFacade LogicProject { get; set; }

        public TestHelperSaveTemplate Template { get; set; }

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