using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voodoo.CodeGeneration.Helpers;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public class DetailFile : TypedCodeFile
    {
        public DetailFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            
            PageSpecificUsingStatements.Add("Voodoo.Validation");
            Name = $"{Name}Detail";
            PageSpecificUsingStatements.Add(type.Namespace);


            PageSpecificUsingStatements.Add("System");
            PageSpecificUsingStatements.Add("System.Collections.Generic");
            PageSpecificUsingStatements.Add("System.ComponentModel.DataAnnotations");
            PageSpecificUsingStatements.Add("System.Linq");
            PageSpecificUsingStatements.Add("Voodoo.Messages");
            PageSpecificUsingStatements.Add("Voodoo.Infrastructure.Notations");

            type.Properties.ForEach(c => PageSpecificUsingStatements.Add(c.PropertyType.Namespace));
        }
        public override string GetFileContents()
        {
            var output = new StringBuilder();
            foreach (var item in UsingStatements) {
                output.AppendLine($"using {item};");
            }
            output.AppendLine($"namespace {Namespace}");
            output.AppendLine("{");
            output.AppendLine($"public class {Type.DetailMessageName}");
            output.AppendLine("{");
            foreach (var item in Type.DetailMessageProperties) {
                foreach (var attr in item.Property.Attributes) {
                    output.AppendLine(attr.Text);
                }
                output.AppendLine($"public {item.StringifiedTypeName} {item.PropertyName} {{get;set;}}");
                output.AppendLine();
            }
            output.AppendLine("}");
            output.AppendLine("}");
            var code = CodeFormatter.Format(output.ToString());
            return code;
    }
    

        public override string GetFolder()
        {
            return $@"Operations\{ExtrasFolder}";
        }
    }
}
