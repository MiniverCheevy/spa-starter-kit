﻿using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Tests
{
    public partial class AddCommandTestsTemplate
    {
        public AddCommandTestsFile File { get; set; }
    }

    public class AddCommandTestsFile : TypedTestFile
    {
        public AddCommandTestsFile(ProjectFacade project, TypeFacade type, ProjectFacade logic)
            : base(project, type)
        {
            Template = new AddCommandTestsTemplate {File = this};
            Name = string.Format("{0}AddCommandTests", Name);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName + ".Extras");
            PageSpecificUsingStatements.Add($"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
            PageSpecificUsingStatements.Add("Voodoo.TestData");
        }

        public AddCommandTestsTemplate Template { get; set; }

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