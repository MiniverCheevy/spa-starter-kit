using System.Collections.Generic;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.VisualStudio;
using Voodoo;
using Voodoo.CodeGeneration.Models.Reflection;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public partial class ExtensionTemplate
    {
        public ExtensionFile File { get; set; }
    }

    public interface IExtensionFile
    {
    }

    public class ExtensionFile : TypedCodeFile, IExtensionFile
    {
        public ExtensionFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new ExtensionTemplate {File = this};
            Name = $"{Name}Extensions";

            PageSpecificUsingStatements.Add(type.SystemType.Namespace);
            if (HasContext)
                PageSpecificUsingStatements.Add(ContextNamespace);
            Mappings = MappingFactory.GetMappings(type, project);
            Mappings.ForEach(c => PageSpecificUsingStatements.AddIfNotNullOrWhiteSpace(c.Namespace));
            PageSpecificUsingStatements.Add($"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
        }

        public List<MappingFactory.Mapping> Mappings { get; set; }
        public ExtensionTemplate Template { get; set; }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return $@"Operations\{ExtrasFolder}";
        }

        public class EmptyClass
        {
        }
    }
}