using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel
{

    public class ListQueryFile : TypedCodeFile
    {


        public ListQueryFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {

            Name = $"{Name}ListQuery";

            PageSpecificUsingStatements.Add(type.Namespace);
            PageSpecificUsingStatements.Add($"{Namespace}.Extras");
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Models.Mappings");

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
                PageSpecificUsingStatements.Add("Microsoft.EntityFrameworkCore");
            }
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
            output.AppendLine($"[Rest(Verb.Get, RestResources.{Type.Name}List)]");
            output.AppendLine($"public class {Name}:QueryAsync<{Type.Name}ListRequest,{Type.Name}ListResponse>");
            output.AppendLine("{");
            if (HasContext)
            {
                output.AppendLine($"private {ContextName} context;");
            }

            output.AppendLine("private IValidator validator = ValidationManager.GetDefaultValidatitor();");
            output.AppendLine($"public {Name} ({Type.Name}ListRequest request) : base(request)");
            output.AppendLine("{");
            output.AppendLine("}");

            output.AppendLine($"protected override async Task<{Type.Name}ListResponse> ProcessRequestAsync()");
            output.AppendLine("{");

            if (HasContext)
            {
                output.AppendLine("using (context = IOC.GetContext())");
                output.AppendLine("{");
                output.AppendLine($"var query = context.{Type.PluralName}.AsNoTracking().AsQueryable();");
                output.AppendLine($"var data = await query.ToPagedResponseAsync(request, c => c.To{Type.Name}Row());");
                output.AppendLine("response.From(data, c=>c);");
                output.AppendLine("}");
                output.AppendLine("return response;");
            }
            else
            {
                output.AppendLine($"throw new NotImplementedException();");
                
            }
            
            output.AppendLine("}");
            output.AppendLine("}");
            output.AppendLine("}");
            var code = CodeFormatter.Format(output.ToString());
            return code;
        }

        public override string GetFolder()
        {
            return $@"Operations\{PluralName}";
        }
    }
}