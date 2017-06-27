using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Tests
{
    public partial class DeleteCommandTestsTemplate
    {
        public DeleteCommandTestsFile File { get; set; }
    }

    public class DeleteCommandTestsFile : TypedTestFile
    {
        public DeleteCommandTestsTemplate Template { get; set; }

        public DeleteCommandTestsFile(ProjectFacade project, TypeFacade type, ProjectFacade logic)
            : base(project, type)
        {
            Template = new DeleteCommandTestsTemplate {File = this};
            Name = string.Format("{0}DeleteCommandTests", Name);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName + ".Extras");
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
            PageSpecificUsingStatements.Add("Voodoo.TestData");
            PageSpecificUsingStatements.Add("using System");
            PageSpecificUsingStatements.Add("using System.Collections.Generic");
            PageSpecificUsingStatements.Add("using System.Linq");
            PageSpecificUsingStatements.Add("using System.Net.Cache");
            PageSpecificUsingStatements.Add("using System.Text");
            PageSpecificUsingStatements.Add("using System.Threading.Tasks");
            PageSpecificUsingStatements.Add("using Voodoo");
            PageSpecificUsingStatements.Add("using Voodoo.Messages");
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