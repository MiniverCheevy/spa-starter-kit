using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel
{
    public class DeleteCommandFile : TypedCodeFile
    {
        public bool UseSoftDelete { get; set; }

        public DeleteCommandFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Name = $"{Name}DeleteCommand";
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

        public override string GetFolder()
        {
            return $@"Operations\{PluralName}";
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
            output.AppendLine($"[Rest(Verb.Delete, RestResources.{Type.Name})]");
            output.AppendLine($"public class {Name} :CommandAsync<IdRequest,Response>");
            output.Append("{");
            if (HasContext)
            {
                output.AppendLine($"private {ContextName} context;");
            }
            output.AppendLine("private IValidator validator = ValidationManager.GetDefaultValidatitor();");
            output.AppendLine($"public {Name}(IdRequest request) : base(request)");
            output.AppendLine("{");
            output.AppendLine("}");

            output.AppendLine($"protected override async Task<Response> ProcessRequestAsync()");
            output.AppendLine(" {");
            if (HasContext && Type.HasId)
            {
                output.AppendLine($"using(context = IOC.GetContext())");
                output.AppendLine("{");
                output.AppendLine($"	var model = await context.{Type.PluralName}");
                output.AppendLine($"						.FirstOrDefaultAsync(c=>c.Id == request.Id);");
                output.AppendLine($"model.ThrowIfNull({Type.Name}Messages.NotFound);");

                output.AppendLine(UseSoftDelete
                    ? $"	model.IsActive = false;"
                    : $"	context.{Type.PluralName}.Remove(model);");

                output.AppendLine($"	response.NumberOfRowsEffected = await context.SaveChangesAsync();");
                output.AppendLine("}");
                output.AppendLine($"response.Message = {Type.Name}Messages.DeleteOk;");
                output.AppendLine($"return response;");
                output.Append("}");
            }
            else
            {
                output.AppendLine($"throw new NotImplementedException();");
                output.Append("}");
            }
            
            output.AppendLine("}");
            output.AppendLine("}");
            return output.ToString();
        }
    }
}