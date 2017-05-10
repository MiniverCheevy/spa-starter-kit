using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.Rest;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Tests
{
    public partial class TestTemplate
    {
        public TestFile File { get; set; }
    }

    public class TestFile : TypedTestFile
    {
        public ProjectFacade LogicProject { get; set; }

        public Operation Operation { get; set; }

        public TestTemplate Template { get; set; }

        public TestFile(ProjectFacade project, TypeFacade type, ProjectFacade logic)
            : base(project, type)
        {
            Operation = Operation.DiscoverTypes(type.SystemType, new Operation());
            if (Operation != null)
                PageSpecificUsingStatements.Add(Operation.RequestType.Namespace);
            LogicProject = logic;
            Template = new TestTemplate {File = this};
            Name = $"{Name}Tests";
            PageSpecificUsingStatements.Add(ContextNamespace);
            PageSpecificUsingStatements.Add(logic.RootNamespace);
            PageSpecificUsingStatements.Add(Type.Namespace);
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            var folder = Type.Namespace.Remove(0, LogicProject.RootNamespace.Length).TrimStart('.').Replace('.', '\\');
            return folder;
        }
    }
}