using System.Linq;
using System.Text;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public partial class RepositoryTemplate
    {
        public RepositoryFile File { get; set; }
    }

    public class RepositoryFile : TypedCodeFile
    {
        public string FriendlyName { get; set; }
        public RepositoryTemplate Template { get; set; }
        public string[] Errors { get; set; }

        public RepositoryFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            FriendlyName = Name.ToFriendlyString();
            Template = new RepositoryTemplate { File = this };
            Name = $"{Name}Repository";
            PageSpecificUsingStatements.Add(Type.SystemType.Namespace);
            Errors = type.Properties.SelectMany(c => c.ErrorMessages).Select(c => c.Text).ToArray();
            if (HasContext)
            {
                PageSpecificUsingStatements.Add(ContextNamespace);
                PageSpecificUsingStatements.Add("System.Data.Entity");
            }
        }
        public override string GetFolder()
        {
            return $@"Operations\{ExtrasFolder}";
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
            output.AppendLine($"public class {Name}");
            output.AppendLine("{");

            if (HasContext)
            {
                output.AppendLine($"private {ContextName} context;");
                output.AppendLine($"public {Name}({ContextName} context)");
                output.AppendLine("{");
                output.AppendLine("this.context = context;");
                output.AppendLine("}");
            }
            output.AppendLine("}");
            output.AppendLine("}");
            return output.ToString();
        }
    }
}