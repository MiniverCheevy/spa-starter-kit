using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{

    public class ListRequestFile : TypedCodeFile
    {


        public ListRequestFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {

            Name = $"{Name}ListRequest";

            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.ComponentModel.DataAnnotations");
            PageSpecificUsingStatements.Add("System.Linq");
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
            output.AppendLine($"public class {Name} : PagedRequest");
            output.AppendLine("{");


            if (Type.HasActiveFlag)
            {
                output.AppendLine("public bool IncludeInactive {get;set;}");
            }
            var sortMember = "null";
            if (Type.HasName)
                sortMember = "Name";
            else if (Type.HasId)
                sortMember = "Id";
            output.AppendLine($"public override string DefaultSortMember => \"{sortMember}\";");


            output.AppendLine("}");
            output.AppendLine("}");


            return CodeFormatter.Format(output.ToString());

        }

        public override string GetFolder()
        {
            return $@"Operations\{ExtrasFolder}";
        }
    }
}