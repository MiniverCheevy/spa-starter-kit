using System.Collections.Generic;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public partial class MapperTemplate
    {
        public MapperFile File { get; set; }
    }

    public class MapperFile : TypedCodeFile
    {
        public List<MappingFactory.Mapping> Mappings { get; set; } = new List<MappingFactory.Mapping>();
        public MapperTemplate Template { get; set; }

        public MapperFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            OverwriteExistingFile = true;
            Template = new MapperTemplate {File = this};
            Name = string.Format("{0}Extensions", Name);
            PageSpecificUsingStatements.Add(type.SystemType.Namespace);
            Mappings = MappingFactory.GetMappings(type, project);
            Mappings.ForEach(c => PageSpecificUsingStatements.AddIfNotNullOrWhiteSpace(c.Namespace));
            Mappings.ForEach(c => PageSpecificUsingStatements.AddIfNotNullOrWhiteSpace(c.Namespace));
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
        }

        public override string GetFileContents()
        {
            return Template.TransformText();
        }

        public override string GetFolder()
        {
            return string.Format(@"Operations\{0}", ExtrasFolder);
        }
    }
}