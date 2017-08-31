using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel
{
    public partial class SaveCommandTemplate
    {
        public SaveCommandFile File { get; set; }
    }

    public class SaveCommandFile : TypedCodeFile
    {
        public SaveCommandTemplate Template { get; set; }

        public SaveCommandFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            Template = new SaveCommandTemplate {File = this};
            Name = $"{Name}SaveCommand";
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
                PageSpecificUsingStatements.Add("System.Data.Entity");
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
            output.AppendLine($"[Rest(Verb.Post, RestResources.{Type.Name})]");
            output.AppendLine($"public class {Name} :CommandAsync<{Type.DetailMessageName},NewItemResponse>");
            output.AppendLine("{");
            if (HasContext)
            {
                output.AppendLine($"private {ContextName} context;");
            }
            output.AppendLine("private bool isNew = false;");
            output.AppendLine("private IValidator validator = ValidationManager.GetDefaultValidatitor();");
            output.AppendLine($"public {Name}(IdRequest request) : base(request)");
            output.AppendLine("{");
            output.AppendLine("}");

            output.AppendLine($"protected override async Task<NewItemResponse> ProcessRequestAsync()");
            output.AppendLine(" {");
            if (HasContext && Type.HasId)
            {
                output.AppendLine($"using(context = IOC.GetContext())");
                output.AppendLine("{");
                output.AppendLine($"	var model = await createOrGetExisting();");
                output.AppendLine($"model.ThrowIfNull({Type.Name}Messages.NotFound);");

                output.AppendLine($"model.UpdateFrom(request);");
                output.AppendLine($"await context.SaveChangesAsync();");
                output.AppendLine($"");
                output.AppendLine($"response.NewItemId = model.Id;");
                output.AppendLine($"");
                output.AppendLine("}");
                output.AppendLine("response.Message = ");
                output.AppendLine($"isNew ? {Type.Name}Messages.AddOk:{Type.Name}Messages.UpdateOk;");
                output.AppendLine($"return response;");
                output.AppendLine("}");

                output.AppendLine($"protected async Task<{Type.Name}> createOrGetExisting()");
                output.AppendLine(" {");
                output.AppendLine($"if (request.Id == 0)");
                output.AppendLine($"{{");
                output.AppendLine($"isNew=true;");
                output.AppendLine($"var model = new {Type.Name}();");
                output.AppendLine($"context.{Type.PluralName}.Add(model);");
                output.AppendLine($"return model;");
                output.AppendLine("}");
                output.AppendLine($"else");
                output.AppendLine("{");
                output.AppendLine($"return await context.{Type.PluralName}");
                output.AppendLine($".FirstOrDefaultAsync(c=>c.Id == request.Id);");
                output.AppendLine("}");

                output.AppendLine("}");
            }

            output.AppendLine("}");
            output.AppendLine("}");
            return output.ToString();
        }
    }
}