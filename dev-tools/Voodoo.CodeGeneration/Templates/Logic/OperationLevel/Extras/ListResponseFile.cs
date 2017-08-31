using System.Text;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    
    public class ListResponseFile : TypedCodeFile
    {
      

        public ListResponseFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
      
            Name = $"{Name}ListResponse";



            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add("System.Text");
            PageSpecificUsingStatements.Add("System.Threading.Tasks");
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            PageSpecificUsingStatements.Add("Voodoo.Operations.Async");
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
            output.AppendLine($"public class {Name} : PagedResponse<{Type.RowMessageName}>");
            
            output.AppendLine("{");
            output.AppendLine("}");
            output.AppendLine("}");
        
    
            return CodeFormatter.Format(output.ToString());

        }

        public override string GetFolder()
        {
            return string.Format(@"Operations\{0}", ExtrasFolder);
        }
    }
}