using System.Collections.Generic;
using System.Linq;
using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{

    public class ExtensionFile : TypedCodeFile
    {
        public List<MappingFactory.Mapping> mappings { get; set; }

        public ExtensionFile(ProjectFacade project, TypeFacade type, List<MappingFactory.Mapping> mappings)
            : base(project, type)
        {
            this.mappings = mappings.ToArray().Distinct().ToList();
            Name = $"{Name}Extensions";
            PageSpecificUsingStatements.Add(type.SystemType.Namespace);
            if (HasContext)
                PageSpecificUsingStatements.Add(ContextNamespace);
            this.mappings.ForEach(c => PageSpecificUsingStatements.AddIfNotNullOrWhiteSpace(c.Namespace));
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
        }

        public override string GetFolder()
        {
            return $@"Models\Mappings";
        }

        public override string GetFileContents()
        {
            var output = new StringBuilder();
            foreach (var item in UsingStatements)
            {
                output.AppendLine($"using {item};");
            }
            output.AppendLine($"namespace {Namespace}");
            output.AppendLine("{");
            output.AppendLine($"public static partial class {Type.Name}Extensions");
            output.AppendLine("{");

            if (HasContext)
            {
                output.AppendLine($"public static {Type.Name}Repository {Type.Name}Repository(this {ContextName} context)");
                output.AppendLine("{");
                output.AppendLine($"return new {Type.Name}Repository(context);");
                output.AppendLine("}");
            }


            foreach (var map in mappings)
            {
                output.AppendLine($"public static {map.MessageTypeName} To{map.MessageTypeName}(this {map.ModelTypeName} model)");
                output.AppendLine("{");
                output.AppendLine($"var message = to{map.MessageTypeName}(model, new {map.MessageTypeName}());");
                output.AppendLine("return message;	");
                output.AppendLine("}");

                output.AppendLine($"public static {map.ModelTypeName} UpdateFrom(this  {map.ModelTypeName} model, {map.MessageTypeName} message)");
                output.AppendLine("{");
                output.AppendLine($"return updateFrom{map.MessageTypeName}(message, model);");
                output.AppendLine("}");
            }
            output.AppendLine("}");
            output.AppendLine("}");
            return output.ToString();
        }
    }
}