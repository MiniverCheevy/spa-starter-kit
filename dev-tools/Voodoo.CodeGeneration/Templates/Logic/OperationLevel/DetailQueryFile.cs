﻿using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel
{
    public partial class DetailQueryTemplate
    {
        public DetailQueryFile File { get; set; }
    }

    public class DetailQueryFile : TypedCodeFile
    {
        public string MapMethodName { get; set; }
        public string MessageName { get; set; }
        public DetailQueryTemplate Template { get; set; }

        public DetailQueryFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new DetailQueryTemplate {File = this};
            Name = $"{Name}DetailQuery";
            PageSpecificUsingStatements.Add(type.Namespace);
            PageSpecificUsingStatements.Add($"{Namespace}.Extras");
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");


            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add("System.Threading.Tasks");
            PageSpecificUsingStatements.Add("Voodoo");
            PageSpecificUsingStatements.Add("Voodoo.Infrastructure");
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            PageSpecificUsingStatements.Add("Voodoo.Operations");
            PageSpecificUsingStatements.Add("Voodoo.Operations.Async");
            PageSpecificUsingStatements.Add("Voodoo.Validation.Infrastructure");
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
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
            return $@"Operations\{PluralName}";
        }
    }
}