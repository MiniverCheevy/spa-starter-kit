using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Tests
{

    public class MappingTestsFile : TypedTestFile
    {
        private MappingFactory.Mapping map;

        public MappingTestsFile(ProjectFacade project, TypeFacade type, ProjectFacade logic, MappingFactory.Mapping map)
            : base(project, type)
        {
            this.map = map;
            Name = $"{map.ModelTypeName}{map.MessageTypeName}MappingTests";

            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName);
            PageSpecificUsingStatements.Add(logic.RootNamespace + ".Operations." + PluralName + ".Extras");
            PageSpecificUsingStatements.Add(
        $"{Vs.Helper.Solution.DataProject.RootNamespace}.Operations.{type.PluralName}.Extras");
            PageSpecificUsingStatements.Add(
        $"{Vs.Helper.Solution.DataProject.RootNamespace}.Models.Mappings");
            PageSpecificUsingStatements.Add(type.SystemType.Namespace);
            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add("System.Net.Cache");
            PageSpecificUsingStatements.Add("System.Text");
            PageSpecificUsingStatements.Add("System.Threading.Tasks");
            PageSpecificUsingStatements.Add("Voodoo");
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            PageSpecificUsingStatements.Add("Voodoo.TestData");


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
            output.AppendLine(Tests.ClassLevelAttribute);
            output.AppendLine($"public class {Name}");
            output.AppendLine("{");
            output.AppendLine("");
            output.AppendLine($"private Randomizer randomizer = new Randomizer();  ");
            output.AppendLine($"   ");
            output.AppendLine($"private {Type.Name} arrange()");
            output.AppendLine("{ ");
            output.AppendLine($"TestHelper.SetRandomDataSeed(1);");
            output.AppendLine($"var source = new {Type.Name}();");
            output.AppendLine($"TestHelper.Randomizer.Randomize(source);");
            output.AppendLine($"return source; ");
            output.AppendLine("} ");
            output.AppendLine("");
            output.AppendLine(Tests.TestLevelAttribute);
            output.AppendLine($"public void Map_MapBack_PropertiesTheSame()");
            output.AppendLine("{");
            output.AppendLine($"var testHelper =     ");
            output.AppendLine($"new MappingTesterHelper<{map.ModelTypeName}, {map.MessageTypeName}>();    ");
            output.AppendLine($"var source = arrange();");
            output.AppendLine($"var message = source.To{map.MessageTypeName}();       ");
            output.AppendLine($"var target = new {Type.Name}();");
            output.AppendLine($"target.UpdateFrom(message);    ");
            if (Type.HasId)
            {             
                output.AppendLine("testHelper.Compare(source, message, new string[]{});");
                output.AppendLine("testHelper.Compare(target, message, new[] { \"Id\" }); ");
            }
            else
            {
                output.AppendLine("testHelper.Compare(source, message, new string[]{}); ");
                output.AppendLine("testHelper.Compare(target, message, new string[]{}); ");                
            }
            output.AppendLine("}");
            output.AppendLine("}");
            output.AppendLine("}");
            return output.ToString();
        }
    }
}


