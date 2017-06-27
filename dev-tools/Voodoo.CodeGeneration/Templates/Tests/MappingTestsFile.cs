using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Tests
{
    public partial class MappingTestsTemplate
    {
        public MappingTestsFile File { get; set; }
    }

    public class MappingTestsFile : TypedTestFile
    {
        public MappingTestsTemplate Template { get; set; }

        public MappingTestsFile(ProjectFacade project, TypeFacade type, ProjectFacade logic)
            : base(project, type)
        {
            Template = new MappingTestsTemplate {File = this};
            Name = string.Format("{0}MappingTests", Name);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName + ".Extras");
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
            PageSpecificUsingStatements.Add(type.SystemType.Namespace);
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