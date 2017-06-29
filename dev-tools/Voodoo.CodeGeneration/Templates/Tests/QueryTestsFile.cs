using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Tests
{

    public class QueryTestsFile : TypedTestFile
    {
        public QueryTestsFile(ProjectFacade project, TypeFacade type, ProjectFacade logic)
            : base(project, type)
        {

            Name = $"{Name}QueryTests";
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName + ".Extras");
            PageSpecificUsingStatements.Add(
                $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
            PageSpecificUsingStatements.Add(ContextNamespace);
            PageSpecificUsingStatements.Add(logic.RootNamespace);
            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add("System.Net.Cache");
            PageSpecificUsingStatements.Add("System.Text");
            PageSpecificUsingStatements.Add("System.Threading.Tasks");
            PageSpecificUsingStatements.Add("Voodoo");
            PageSpecificUsingStatements.Add("Voodoo.Messages");
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
           
            output.AppendLine($"{Tests.ClassLevelAttribute}");
            output.AppendLine($"public class {Name}");
            output.AppendLine("{");
            output.AppendLine();
            output.AppendLine($"{Tests.TestLevelAttribute}");
            output.AppendLine($"public async Task { Type.Name }DetailQuery_ValidRequest_IsOk()");
            output.AppendLine("{");

            if (Type.HasId)
            {

                output.AppendLine("int? id = 0;");
                output.AppendLine("using (var context = IOC.GetContext())");
                output.AppendLine("{");
                output.AppendLine($"if (context.{ Type.PluralName}.Any())");
                output.AppendLine($"id = context.{ Type.PluralName}.Max(c => c.Id);");
                output.AppendLine("}");
                output.AppendLine($"id.Should().HaveValue().Should().NotBe(0,\"No data in {Type.Name} table\");");
                output.AppendLine($"var request = new IdRequest {{ Id = id.Value }};");
                output.AppendLine($"var response = await new { Type.Name }DetailQuery(request).ExecuteAsync();");
                output.AppendLine($"response.Details.Should().BeEmpty();");
                output.AppendLine($"response.Message.Should().BeNull();");
                output.AppendLine($"response.IsOk.Should().BeTrue();");
                output.AppendLine($"response.Data.Should().NotBeNull();");


            }
            output.AppendLine("}");

            output.AppendLine($"public {Type.Name}ListRequest getValidRequest()");
            output.AppendLine("{");
            output.AppendLine($"return new {Type.Name}ListRequest();");
            output.AppendLine("}");
            output.AppendLine();
            output.AppendLine($"{ Tests.TestLevelAttribute} ");
            output.AppendLine($"public async Task { Type.Name }ListQuery_ValidRequest_IsOk()");
            output.AppendLine("{");
            output.AppendLine($"var request = getValidRequest();");
            output.AppendLine($"var response = await new { Type.Name }ListQuery(request).ExecuteAsync();");
            output.AppendLine($"response.Details.Should().BeEmpty();");
            output.AppendLine($"response.Message.Should().BeNull();");
            output.AppendLine($"response.IsOk.Should().BeTrue();");
            output.AppendLine($"response.Data.Should().NotBeNull();");
            output.AppendLine($"response.Data.Should().NotBeEmpty();");
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