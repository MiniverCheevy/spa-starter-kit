using System;
using System.Linq;
using System.Text;
using Voodoo.CodeGeneration.Models;
using Voodoo.CodeGeneration.Models.Reflection;
using Voodoo.CodeGeneration.Models.VisualStudio;

namespace Voodoo.CodeGeneration.Templates.Logic.OperationLevel.Extras
{
    public partial class MessagesTemplate
    {
        public MessagesFile File { get; set; }
    }

    public class MessagesFile : TypedCodeFile
    {
        public string FriendlyName { get; set; }
        public MessagesTemplate Template { get; set; }
        public string[] Errors { get; set; }

        public MessagesFile(ProjectFacade project, TypeFacade type)
            : base(project, type)
        {
            FriendlyName = Name.ToFriendlyString();
            Template = new MessagesTemplate { File = this };
            Name = $"{Name}Messages";
            PageSpecificUsingStatements.Add(Type.SystemType.Namespace);
            Errors = type.Properties.SelectMany(c => c.ErrorMessages).Select(c => c.Text).ToArray();
            type.Properties.ForEach(c => PageSpecificUsingStatements.Add(c.PropertyType.Namespace));
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
            output.AppendLine($"public struct {Name}");
            output.AppendLine("{");
            output.AppendLine($"public const string AddOk = \"{FriendlyName} added successfully.\";");
            output.AppendLine($"public const string UpdateOk = \"{FriendlyName} updated successfully.\";");
            output.AppendLine($"public const string DeleteOk = \"{FriendlyName} deleted successfully.\";");
            output.AppendLine($"public const string NotFound = \"{FriendlyName} not found.\";");

            foreach (var item in Errors)
            {
                output.AppendLine($"{item}");
            }
            output.AppendLine("}");
            output.AppendLine("}");
            return output.ToString();
        }
    }
}